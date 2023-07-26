using System.Collections.Generic;
using Source.StateMachine.Transitions;
using UnityEngine;

namespace Source.StateMachine.States
{
    public abstract class State : MonoBehaviour
    {
        [SerializeField] private List<Transition> _transitions;
        
        protected Transform Target { get; set; }

        public void Enter(Transform target)
        {
            if (enabled) 
                return;
            
            Target = target;

            enabled = true;

            foreach (Transition transition in _transitions)
            {
                transition.Init(target);
                transition.enabled = true;
            }
        }
        
        public void Exit()
        {
            if (enabled == false) 
                return;
            
            foreach (Transition transition in _transitions)
                transition.enabled = false;

            enabled = false;
        }

        public State GetNextState()
        {
            if (_transitions.Count > 0)
            {
                foreach (Transition transition in _transitions)
                {
                    if (transition.NeedTransition)
                        return transition.TargetState;
                }
            }

            return null;
        }
    }
}