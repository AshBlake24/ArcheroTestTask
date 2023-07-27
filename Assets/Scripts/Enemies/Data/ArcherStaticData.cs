using Source.Combat;
using UnityEngine;

namespace Source.Enemies.Data
{
    [CreateAssetMenu(fileName = "New Archer", menuName = "Static Data/Enemies/Archer")]
    public class ArcherStaticData : EnemyStaticData
    {
        [SerializeField, Min(0)] private int _maxShotsSeries;
        [SerializeField, Min(0)] private float _attackRate;
        [SerializeField, Min(0)] private float _attackForce;
        [SerializeField] private Projectile _projectilePrefab;
        
        public int MaxShotsSeries => _maxShotsSeries;
        public float AttackRate => _attackRate;
        public float AttackForce => _attackForce;
        public Projectile ProjectilePrefab => _projectilePrefab;
    }
}