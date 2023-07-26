using System;
using System.Collections.Generic;
using Source.Behaviour.States;

namespace Source.Behaviour
{
    public class StateMachine
    {
        private static readonly List<Transition> s_emptyTransitions = new List<Transition>(capacity: 0);
        
        private readonly Dictionary<Type, List<Transition>> _transitions = new Dictionary<Type, List<Transition>>();
        private readonly List<Transition> _anyTransitions = new List<Transition>();
        private List<Transition> _currentTransitions;
        private IState _currentState;

        public void Tick()
        {
            Transition transition = GetTransition();

            if (transition != null)
                SetState(transition.TargetState);

            _currentState?.Tick();
        }

        public void AddTransition(IState from, IState to, Func<bool> predicate)
        {
            Type key = from.GetType();
            
            if (_transitions.TryGetValue(key, out List<Transition> transitions) == false)
            {
                transitions = new List<Transition>();
                _transitions[key] = transitions;
            }
            
            transitions.Add(new Transition(to, predicate));
        }

        public void AddAnyTransition(IState to, Func<bool> predicate)
        {
            _anyTransitions.Add(new Transition(to, predicate));
        }

        private void SetState(IState state)
        {
            if (state == _currentState)
                return;
            
            _currentState?.OnExit();
            _currentState = state;

            _transitions.TryGetValue(_currentState.GetType(), out _currentTransitions);
            _currentTransitions ??= s_emptyTransitions;
            
            _currentState.OnEnter();
        }

        private Transition GetTransition()
        {
            foreach (Transition transition in _anyTransitions)
                if (transition.Condition())
                    return transition;
            
            foreach (Transition transition in _currentTransitions)
                if (transition.Condition())
                    return transition;

            return null;
        }
    }
}