using System;
using Source.Logic;
using UnityEngine;

namespace Source.Player.Components
{
    public class PlayerHealth : MonoBehaviour, IHealth
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

            _currentValue = Mathf.Max(CurrentValue - damage, 0);
            
            HealthChanged?.Invoke();
        }
    }
}