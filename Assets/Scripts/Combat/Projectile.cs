using Source.Logic;
using Source.Utilities;
using UnityEngine;

namespace Source.Combat
{
    [RequireComponent(typeof(Rigidbody))]
    public class Projectile : MonoBehaviour, IPoolable<Projectile>
    {
        [SerializeField] private Rigidbody _rigidbody;

        private int _damage;
        private float _force;
        private ObjectPool<Projectile> _projectilesPool;

        public void Init(int damage, float force)
        {
            _rigidbody.velocity = Vector3.zero;
            _damage = damage;
            _force = force;
            _rigidbody.AddForce(transform.forward * _force, ForceMode.Impulse);
        }

        public void SetPool(ObjectPool<Projectile> projectilesPool) => 
            _projectilesPool = projectilesPool;

        private void OnTriggerEnter(Collider other)
        {
            TryDealDamage(other.gameObject);
            _projectilesPool.Release(this);
        }

        private void TryDealDamage(GameObject obj)
        {
            if (obj.TryGetComponent(out IHealth health))
                health.TakeDamage(_damage);
        }
    }
}