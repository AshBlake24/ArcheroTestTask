using Source.Behaviour.Transitions;
using Source.Enemies.Data;
using UnityEngine;
using UnityEngine.AI;

namespace Source.Behaviour.States
{
    public class MoveAgentToRandomPoint : IState
    {
        public const float MinimalDistanceToPoint = 0.5f;
        
        private readonly NavMeshAgent _navMeshAgent;
        private readonly ArcherStaticData _enemyData;

        public Transform Self { get; }
        public Vector3 Destination { get; private set; }

        public MoveAgentToRandomPoint(NavMeshAgent navMeshAgent, Transform self, ArcherStaticData enemyData)
        {
            _navMeshAgent = navMeshAgent;
            _enemyData = enemyData;
            Self = self;
        }

        public void Tick()
        {
            if (Destination == Vector3.zero)
                SetDestination();
            else
                _navMeshAgent.SetDestination(Destination);
        }

        public void OnExit() => 
            _navMeshAgent.enabled = false;

        public void OnEnter()
        {
            Destination = Vector3.zero;
            _navMeshAgent.enabled = true;
            _navMeshAgent.speed = _enemyData.Speed;
        }

        private void SetDestination()
        {
            Vector3 randomDirection = Random.insideUnitSphere * _enemyData.MaxMoveDistance;
            randomDirection += Self.position;

            if (NavMesh.SamplePosition(
                    randomDirection,
                    out NavMeshHit hit,
                    Random.Range(_enemyData.MaxMoveDistance / 2, _enemyData.MaxMoveDistance),
                    1))
            {
                Destination = hit.position;
            }
        }
    }
}