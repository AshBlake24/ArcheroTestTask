using Source.Enemies.Data;
using Source.Infrastructure.Services;
using UnityEngine;

namespace Source.Enemies.Factories
{
    public interface IEnemyFactory : IService
    {
        Enemy CreateEnemy(EnemyId id, Vector3 spawnPoint, Transform parent, Transform target);
        Enemy CreateRandomEnemy(Vector3 spawnPoint, Transform target, Transform parent);
    }
}