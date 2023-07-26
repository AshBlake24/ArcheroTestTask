using System;
using Source.Behaviour;
using Source.Behaviour.States;
using Source.Behaviour.Transitions;
using Source.Enemies.Data;
using UnityEngine;
using UnityEngine.AI;

namespace Source.Enemies.Factories
{
    public class EnemyBehaviourFactory : IEnemyBehaviourFactory
    {
        public StateMachine CreateEnemyBehaviour(EnemyStaticData enemyData, Transform target, Enemy enemy)
        {
            return enemyData.Id switch
            {
                EnemyId.Bat => CreateBatStateMachine(enemyData as BatStaticData, enemy, target),
                EnemyId.Capsule => CreateCapsuleStateMachine(enemyData, enemy, target),
                _ => throw new ArgumentOutOfRangeException(nameof(enemyData.Id), "This enemy doesn't exist")
            };
        }

        private StateMachine CreateCapsuleStateMachine(EnemyStaticData enemyData,  Enemy enemy, Transform target)
        {
            StateMachine stateMachine = new StateMachine();

            NavMeshAgent navMeshAgent = enemy.GetComponent<NavMeshAgent>();
            navMeshAgent.enabled = false;

            var moveTowardsTarget = new MoveAgentToTarget(navMeshAgent, target, enemy.transform, enemyData.Speed);
            var idle = new IdleState();

            stateMachine.AddTransition(moveTowardsTarget, idle, 
                TransitionConditions.TargetReached(moveTowardsTarget, 0.3f));
            
            stateMachine.AddAnyTransition(moveTowardsTarget, 
                TransitionConditions.TargetTooFar(enemy.transform, target.transform, 0.3f));

            stateMachine.SetState(moveTowardsTarget);

            return stateMachine;
        }

        private StateMachine CreateBatStateMachine(BatStaticData enemyData, Enemy enemy, Transform target)
        {
            StateMachine stateMachine = new StateMachine();

            var moveTowardsTarget = new MoveTransformToTarget(target, enemy.transform, enemyData.Speed);
            var dash = new Dash(target, enemy.transform, enemyData.DashSpeed);
            var idle = new IdleState();

            DashTimer dashTimer = enemy.GetComponent<DashTimer>();
            dashTimer.Construct(enemyData.DashDuration, enemyData.DashRate);

            stateMachine.AddTransition(moveTowardsTarget, dash, TransitionConditions.DashStarted(dashTimer));
            stateMachine.AddTransition(dash, moveTowardsTarget, TransitionConditions.DashTimerIsOut(dashTimer));
            
            stateMachine.SetState(moveTowardsTarget);

            return stateMachine;
        }
    }
}