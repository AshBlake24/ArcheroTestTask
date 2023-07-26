using System;
using Source.StateMachine.States;
using UnityEngine;

namespace Source.StateMachine
{
    public class EnemyStateMachine : MonoBehaviour
    {
        [SerializeField] private State _startState;

        private State _currentState;
        private Transform _target;

        public void Initialize(Transform target)
        {
            if (target == null)
                throw new ArgumentNullException(nameof(target));
            
            _target = target;
            ResetState(_startState);
        }

        private void Update()
        {
            if (_currentState == null)
                return;
            
            State nextState = _currentState.GetNextState();

            if (nextState != null)
                ChangeState(nextState);
        }

        private void ChangeState(State state)
        {
            if (_currentState != null)
                _currentState.Exit();

            _currentState = state;

            if (_currentState != null)
                _currentState.Enter(_target);
        }

        private void ResetState(State startState) => 
            ChangeState(startState);
    }
}