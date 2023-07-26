using Source.Infrastructure.Services.StaticData;
using UnityEngine;

namespace Source.Enemies.Data
{
    [CreateAssetMenu(fileName = "New Bat", menuName = "Static Data/Enemies/Bat")]
    public class BatStaticData : EnemyStaticData, IStaticData
    {
        [SerializeField] private float _dashSpeed;
        [SerializeField] private float _dashDuration;
        [SerializeField] private float _dashRate;

        public float DashSpeed => _dashSpeed;
        public float DashDuration => _dashDuration;
        public float DashRate => _dashRate;
    }
}