using System;
using Source.Combat;
using UnityEngine;

namespace Source.Enemies
{
    public class EnemyHealth : MonoBehaviour, IDamageable
    {
        public int _maxValue;
        public int _currentValue;

        public event Action HealthChanged;

        public int CurrentValue => _currentValue;
        
        private void Start() => 
            _currentValue = _maxValue;

        public void TakeDamage(int damage)
        {
            if (damage < 0)
                throw new ArgumentOutOfRangeException(nameof(damage), "Damage must not be less than 0");

            _currentValue = Mathf.Max(CurrentValue - damage, 0);
            
            HealthChanged?.Invoke();
        }
    }
}