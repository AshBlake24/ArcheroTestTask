using Source.Combat;
using UnityEngine;

namespace Source.Enemies.Data
{
    [CreateAssetMenu(fileName = "New Archer", menuName = "Static Data/Enemies/Archer")]
    public class ArcherStaticData : EnemyStaticData
    {
        [SerializeField, Min(0)] private int _maxShotsSeries;
        [SerializeField, Min(0)] private float _attackForce;
        [SerializeField, Min(0)] private float _maxMoveDistance;
        [SerializeField, Min(0)] private float _maxIdleTime;
        [SerializeField] private Projectile _projectilePrefab;
        
        public int MaxShotsSeries => _maxShotsSeries;
        public float AttackForce => _attackForce;
        public float MaxMoveDistance => _maxMoveDistance;
        public float MaxIdleTime => _maxIdleTime;
        public Projectile ProjectilePrefab => _projectilePrefab;
    }
}