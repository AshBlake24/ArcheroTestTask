using System;
using System.Timers;
using UnityEngine;

namespace Source.Combat
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private int _damage;
        [SerializeField] private float _speed;

        private void FixedUpdate()
        {
            transform.Translate(Vector3.forward * _speed * Time.fixedDeltaTime);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.TryGetComponent(out IDamageable damageable)) 
                damageable.TakeDamage(_damage);

            Destroy(gameObject);
        }
    }
}