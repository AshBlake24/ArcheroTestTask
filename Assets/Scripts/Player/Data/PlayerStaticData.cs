using UnityEngine;

namespace Source.Player.Data
{
    [CreateAssetMenu(fileName = "Player", menuName = "Static Data/Player")]
    public class PlayerStaticData : ScriptableObject
    {
        [SerializeField] private int _health;
        [SerializeField] private int _damage;
        [SerializeField] private float _attackRate;
        [SerializeField] private float _attackForce;
        [SerializeField] private float _movementSpeed;

        public int Health => _health;
        public int Damage => _damage;
        public float AttackRate => _attackRate;
        public float AttackForce => _attackForce;
        public float MovementSpeed => _movementSpeed;
    }
}