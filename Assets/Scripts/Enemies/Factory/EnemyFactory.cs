using System;
using Source.Behaviour;
using Source.Infrastructure.Assets;

namespace Source.Enemies.Factory
{
    public class EnemyFactory : IEnemyFactory
    {
        private readonly IAssetProvider _assetProvider;

        public EnemyFactory(IAssetProvider assetProvider)
        {
            _assetProvider = assetProvider;
        }

        public Enemy CreateEnemy(EnemyId id)
        {
            return id switch
            {
                EnemyId.Bat => CreateBat(),
                EnemyId.Capsule => CreateCapsule(),
                _ => throw new ArgumentOutOfRangeException(nameof(id))
            };
        }

        private Enemy CreateBat()
        {
            return null;
        }

        private Enemy CreateCapsule()
        {
            // Enemy enemy = _assetProvider.Instantiate(AssetPath.EnemyPath)
            //     .GetComponent<Enemy>();
            //
            // StateMachine stateMachine = new StateMachine();
            //
            // NavMeshAgent navMeshAgent = enemy.GetComponent<NavMeshAgent>();
            // navMeshAgent.enabled = false;
            // navMeshAgent.speed = 3;
            //
            // var moveTowardsTarget = new MoveTransformToTarget(target, enemy.transform, 3f);
            // var idle = new IdleState();
            //
            // stateMachine.AddTransition(moveTowardsTarget, idle, TargetReached);
            // stateMachine.AddAnyTransition(moveTowardsTarget, TargetTooFar);
            //
            // bool TargetReached() => moveTowardsTarget.RemainingDistance < 0.3f;
            // bool TargetTooFar() => Vector3.Distance(enemy.transform.position, enemy.Target.position) > 0.3f;
            //
            // stateMachine.SetState(moveTowardsTarget);
            //
            // enemy.Construct(stateMachine, target);
            //
            // return enemy;

            return null;
        }
    }
}