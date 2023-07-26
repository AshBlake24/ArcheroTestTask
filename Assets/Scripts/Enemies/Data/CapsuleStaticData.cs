using Source.Infrastructure.Services.StaticData;
using UnityEngine;

namespace Source.Enemies.Data
{
    [CreateAssetMenu(fileName = "New Capsule", menuName = "Static Data/Enemies/Capsule")]
    public class CapsuleStaticData : EnemyStaticData, IStaticData
    {
    }
}