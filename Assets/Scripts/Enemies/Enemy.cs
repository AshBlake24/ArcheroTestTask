using Source.Behaviour;
using Source.Gameplay;
using Source.Infrastructure.Events;
using UnityEngine;

namespace Source.Enemies
{
    public class Enemy : MonoBehaviour, IStartGameHandler
    {
        private StateMachine _stateMachine;
        private bool _isActive;

        public void Construct(StateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        private void OnEnable() => EventBus.Subscribe(this);

        private void OnDisable() => EventBus.Unsubscribe(this);

        private void Update()
        {
            if (_isActive)
                _stateMachine.Tick();
        }

        public void OnGameStarted() => 
            _isActive = true;
    }
}