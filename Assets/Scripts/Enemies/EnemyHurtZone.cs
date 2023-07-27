using Source.Logic;
using Source.Player.Components;
using UnityEngine;

namespace Source.Enemies
{
    public class EnemyHurtZone : MonoBehaviour
    {
        [SerializeField] private TriggerObserver _triggerObserver;
        
        private int _damage;
        private float _attackRate;
        private float _elapsedTime;

        public void Construct(int damage, float attackRate)
        {
            _damage = damage;
            _attackRate = attackRate;
            
            _triggerObserver.TriggerStayed += OnStayed;
        }

        private void OnDestroy() => 
            _triggerObserver.TriggerStayed -= OnStayed;

        private void Update() => 
            _elapsedTime += Time.deltaTime;

        private void OnStayed(Collider other)
        {
            if (_elapsedTime < _attackRate)
                return;

            PlayerHealth playerHealth = other.GetComponentInChildren<PlayerHealth>();
            
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(_damage);
                _elapsedTime = 0;
            }
        }
    }
}