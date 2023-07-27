using System;
using UnityEngine;

namespace Source.Enemies
{
    public class EnemyDeath : MonoBehaviour
    {
        [SerializeField] private EnemyHealth _health;
        [SerializeField] private GameObject _deathVFX;
        [SerializeField] private Transform _deathVFXPoint;

        public event Action<EnemyDeath> Died;

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
            Instantiate(_deathVFX, _deathVFXPoint.position, Quaternion.identity);
    }
}