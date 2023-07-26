using System;
using Source.Behaviour.States;

namespace Source.Behaviour
{
    public class Transition
    {
        public IState TargetState { get; }
        public Func<bool> Condition { get; }

        public Transition(IState targetState, Func<bool> condition)
        {
            TargetState = targetState;
            Condition = condition;
        }
    }
}