using System;
using UnityEngine;

namespace Source.Enemies
{
    public class EnemyHealth : MonoBehaviour
    {
        public int _maxValue;
        public int _currentValue;

        public event Action HealthChanged;

        public int CurrentValue
        {
            get => _currentValue;
            private set
            {
                if (_currentValue != value)
                {
                    _currentValue = value;
                    HealthChanged?.Invoke();
                }
            }
        }

        public int MaxValue
        {
            get => _maxValue;
            private set => _maxValue = value;
        }
    }
}