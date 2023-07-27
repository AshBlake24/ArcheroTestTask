using System;
using UnityEngine;

namespace Source.Player.Components
{
    public class PlayerDeath : MonoBehaviour
    {
        [SerializeField] private PlayerHealth _health;
        [SerializeField] private GameObject _deathVFX;

        public event Action<PlayerDeath> Died;

        private void Start() => 
            _health.HealthChanged += OnHealthChanged;

        private void OnDestroy() => 
            _health.HealthChanged -= OnHealthChanged;

        private void OnHealthChanged()
        {
            if (_health.CurrentValue <= 0)
                Die();
        }

        private void Die()
        {
            Died?.Invoke(this);
            
            SpawnDeathFx();
            Destroy(gameObject);
        }

        private void SpawnDeathFx() => 
            Instantiate(_deathVFX, transform.position, Quaternion.identity);
    }
}