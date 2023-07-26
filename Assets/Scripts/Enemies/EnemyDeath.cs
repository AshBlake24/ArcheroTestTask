using System;
using System.Collections;
using Source.Utilities;
using UnityEngine;

namespace Source.Enemies
{
    [RequireComponent(typeof(EnemyHealth))]
    public class EnemyDeath : MonoBehaviour
    {
        [SerializeField] private EnemyHealth _health;
        [SerializeField] private float _timeToDestroy = 3f;

        public event Action Died;

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
            StartCoroutine(DestroyTimer());
            
            Died?.Invoke();
        }

        private IEnumerator DestroyTimer()
        {
            yield return TimeUtility.GetTime(_timeToDestroy);
            
            Destroy(gameObject);
        }
    }
}