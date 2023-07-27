using Source.Behaviour;
using UnityEngine;

namespace Source.Enemies
{
    public class Enemy : MonoBehaviour
    {
        private StateMachine _stateMachine;
        private bool _isActive;

        public void Construct(StateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }
        
        private void Update()
        {
            if (_isActive)
                _stateMachine.Tick();
        }
    }
}