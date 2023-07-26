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
            if (collision.gameObject.TryGetComponent(out IDamageable damageable)) 
                damageable.TakeDamage(_damage);

            Destroy(gameObject);
        }
    }
}