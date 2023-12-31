﻿using Source.Behaviour;
using Source.Gameplay;
using Source.Infrastructure.Events;
using UnityEngine;

namespace Source.Enemies
{
    public class Enemy : MonoBehaviour, IStartGameHandler, IGameOverHandler
    {
        private StateMachine _stateMachine;
        private bool _isActive;
        
        public int Coins { get; private set; }

        public void Construct(StateMachine stateMachine, int coins)
        {
            _stateMachine = stateMachine;
            Coins = coins;
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

        public void OnGameOver() => 
            _isActive = false;
    }
}