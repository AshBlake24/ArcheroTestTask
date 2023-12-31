﻿using System;
using Source.Combat;
using Source.Logic;
using UnityEngine;

namespace Source.Enemies
{
    public class EnemyHealth : MonoBehaviour, IHealth
    {
        private int _maxValue;
        private int _currentValue;

        public event Action HealthChanged;

        public int CurrentValue => _currentValue;
        public int MaxValue => _maxValue;
        
        private void Start() => 
            _currentValue = _maxValue;

        public void Construct(int health)
        {
            _maxValue = health;
            _currentValue = _maxValue;
            
            HealthChanged?.Invoke();
        }

        public void TakeDamage(int damage)
        {
            if (damage < 0)
                throw new ArgumentOutOfRangeException(nameof(damage), "Damage must not be less than 0");

            _currentValue = Mathf.Max(_currentValue - damage, 0);
            
            HealthChanged?.Invoke();
        }
    }
}