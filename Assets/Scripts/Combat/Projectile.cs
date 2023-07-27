using Source.Logic;
using UnityEngine;

namespace Source.Combat
{
    [RequireComponent(typeof(Rigidbody))]
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rigidbody;

        private int _damage;
        private float _force;

        public void Init(int damage, float force)
        {
            _damage = damage;
            _force = force;
            _rigidbody.AddForce(transform.forward * _force, ForceMode.Impulse);
        }

        private void OnCollisionEnter(Collision collision)
        {
            TryDealDamage(collision.gameObject);
            Destroy(gameObject);
        }

        private void OnTriggerEnter(Collider other)
        {
            TryDealDamage(other.gameObject);
            Destroy(gameObject);
        }

        private void TryDealDamage(GameObject obj)
        {
            if (obj.TryGetComponent(out IHealth health))
                health.TakeDamage(_damage);
        }
    }
}