using Source.StateMachine.States;
using UnityEngine;

namespace Source.StateMachine.Transitions
{
    public abstract class Transition : MonoBehaviour
    {
        [SerializeField] private State _targetState;

        public State TargetState => _targetState;
        public bool NeedTransition { get; protected set; }
        protected Transform Target { get; private set; }

        private void OnEnable() => 
            NeedTransition = false;

        public void Init(Transform target) => 
            Target = target;
    }
}