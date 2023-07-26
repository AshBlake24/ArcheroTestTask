using System;
using Source.Infrastructure.Services.StaticData;
using UnityEngine;

namespace Source.Enemies.Data
{
    public abstract class EnemyStaticData : ScriptableObject, IStaticData
    {
        [SerializeField] private EnemyId _id;
        [SerializeField] private EnemyMovementType _movementType;
        [SerializeField] private GameObject _prefab;
        [SerializeField, Min(1)] private int _health;
        [SerializeField, Min(0)] private int _damage;
        [SerializeField, Min(0)] private float _speed;

        public Enum Key => _id;
        public EnemyId Id => _id;
        public EnemyMovementType MovementType => _movementType;
        public GameObject Prefab => _prefab;
        public int Health => _health;
        public int Damage => _damage;
        public float Speed => _speed;
    }
}