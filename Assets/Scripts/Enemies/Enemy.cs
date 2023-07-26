using Source.Behaviour;
using UnityEngine;

namespace Source.Enemies
{
    public class Enemy : MonoBehaviour
    {
        private StateMachine _stateMachine;
        private bool _isActive;

        public Transform Target { get; private set; }

        public void Construct(StateMachine stateMachine, Transform target)
        {
            _stateMachine = stateMachine;
            Target = target;
            
            _isActive = true;
        }
        
        private void Update()
        {
            if (_isActive)
                _stateMachine.Tick();
        }
    }
}