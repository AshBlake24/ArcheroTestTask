using UnityEngine;
using UnityEngine.AI;

namespace Source.Behaviour.States
{
    public class MoveAgentToTarget : IState
    {
        private readonly NavMeshAgent _navMeshAgent;
        private readonly Transform _target;
        private readonly Transform _self;
        private readonly float _speed;

        public MoveAgentToTarget(NavMeshAgent navMeshAgent, Transform target, Transform self, float speed)
        {
            _navMeshAgent = navMeshAgent;
            _target = target;
            _self = self;
            _speed = speed;
        }

        public float RemainingDistance => Vector3.Distance(_self.position, _target.position);

        public void Tick() => 
            _navMeshAgent.SetDestination(_target.position);

        public void OnExit() => 
            _navMeshAgent.enabled = false;

        public void OnEnter()
        {
            _navMeshAgent.enabled = true;
            _navMeshAgent.speed = _speed;
        }
    }
}