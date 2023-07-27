using System;
using Source.Data;
using Source.Data.Service;
using Source.Logic;
using UnityEngine;

namespace Source.Player.Components
{
    public class PlayerHealth : MonoBehaviour, IHealth, IProgressWriter
    {
        private State _state;

        public event Action HealthChanged;

        
        public int CurrentValue
        {
            get => _state.CurrentHealth;
            private set
            {
                if (_state.CurrentHealth != value)
                {
                    _state.CurrentHealth = value;
                    HealthChanged?.Invoke();
                }
            }
        }
        public int MaxValue
        {
            get => _state.MaxHealth;
            private set
            {
                if (_state.MaxHealth != value)
                {
                    _state.MaxHealth = value;
                    HealthChanged?.Invoke();
                }
            }
        }

        public void TakeDamage(int damage)
        {
            if (damage < 0)
                throw new ArgumentOutOfRangeException(nameof(damage), "Damage must not be less than 0");

            CurrentValue = Mathf.Max(CurrentValue - damage, 0);
        }

        public void ReadProgress(PlayerProgress progress)
        {
            _state = progress.State;
            HealthChanged?.Invoke();
        }

        public void WriteProgress(PlayerProgress progress)
        {
            progress.State.CurrentHealth = CurrentValue;
            progress.State.MaxHealth = MaxValue;
        }
    }
}