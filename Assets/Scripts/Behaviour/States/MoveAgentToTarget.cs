using UnityEngine;
using UnityEngine.AI;

namespace Source.Behaviour.States
{
    public class MoveAgentToTarget : MoveToTarget
    {
        private readonly NavMeshAgent _navMeshAgent;

        public MoveAgentToTarget(NavMeshAgent navMeshAgent, Transform target, Transform self, float speed) 
            : base(target, self, speed)
        {
            _navMeshAgent = navMeshAgent;
        }

        public override void Tick() => 
            _navMeshAgent.SetDestination(Target.position);

        public override void OnExit() => 
            _navMeshAgent.enabled = false;

        public override void OnEnter()
        {
            base.OnEnter();
            _navMeshAgent.enabled = true;
            _navMeshAgent.speed = CurrentSpeed;
        }
    }
}