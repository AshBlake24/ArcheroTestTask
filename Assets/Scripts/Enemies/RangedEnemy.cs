using UnityEngine;

namespace Source.Enemies
{
    public class RangedEnemy : Enemy
    {
        [SerializeField] private Transform _shootPoint;

        public Transform ShootPoint => _shootPoint;
    }
}