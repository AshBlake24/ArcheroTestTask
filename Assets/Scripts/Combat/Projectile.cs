using UnityEngine;

namespace Source.Combat
{
    [RequireComponent(typeof(Rigidbody))]
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField]  private float _force;
        [SerializeField] private int _damage;

        private void Start()
        {
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
            if (obj.TryGetComponent(out IDamageable damageable))
                damageable.TakeDamage(_damage);
        }
    }
}