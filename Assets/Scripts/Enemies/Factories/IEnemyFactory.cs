using Source.Enemies.Data;
using Source.Infrastructure.Services;
using UnityEngine;

namespace Source.Enemies.Factories
{
    public interface IEnemyFactory : IService
    {
        Enemy CreateEnemy(EnemyId id, Transform parent, Transform target);
    }
}