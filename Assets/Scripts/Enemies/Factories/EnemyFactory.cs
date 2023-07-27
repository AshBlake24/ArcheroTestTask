using System;
using System.Collections.Generic;
using System.Linq;
using Source.Behaviour;
using Source.Enemies.Data;
using Source.Infrastructure.Services.StaticData;
using Source.Logic;
using Source.UI;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace Source.Enemies.Factories
{
    public class EnemyFactory : IEnemyFactory
    {
        private readonly IStaticDataService _staticDataService;
        private readonly IEnemyBehaviourFactory _enemyBehaviourFactory;

        public EnemyFactory(IStaticDataService staticDataService, IEnemyBehaviourFactory enemyBehaviourFactory)
        {
            _staticDataService = staticDataService;
            _enemyBehaviourFactory = enemyBehaviourFactory;
        }

        public Enemy CreateEnemy(EnemyId id, Vector3 spawnPoint, Transform parent, Transform target)
        {
            if (id == EnemyId.Unknown)
                throw new ArgumentNullException(nameof(id), "Enemy id is not specified");

            EnemyStaticData enemyData = _staticDataService.GetDataById<EnemyId, EnemyStaticData>(id);

            return ConstructEnemy(enemyData, spawnPoint, parent, target);
        }

        public Enemy CreateRandomEnemy(Vector3 spawnPoint, Transform target, Transform parent)
        {
            EnemyStaticData enemyData = GetRandomEnemyData();
            
            return ConstructEnemy(enemyData, spawnPoint, parent, target);
        }

        private EnemyStaticData GetRandomEnemyData()
        {
            List<EnemyStaticData> enemiesStaticData = _staticDataService.GetAllDataByType<EnemyId, EnemyStaticData>()
                .ToList();
            
            return enemiesStaticData[Random.Range(0, enemiesStaticData.Count)];
        }

        private Enemy ConstructEnemy(EnemyStaticData enemyData, Vector3 spawnPoint, Transform parent, Transform target)
        {
            Enemy enemy = Object.Instantiate(enemyData.Prefab, spawnPoint, Quaternion.identity, parent)
                .GetComponent<Enemy>();

            ConstructHealthBar(enemyData, enemy);
            TryConstructHurtZone(enemyData, enemy);
            ConstructEnemyBehaviour(enemyData, target, enemy);

            return enemy;
        }

        private void ConstructEnemyBehaviour(EnemyStaticData enemyData, Transform target, Enemy enemy)
        {
            StateMachine stateMachine = _enemyBehaviourFactory.CreateEnemyBehaviour(enemyData, target, enemy);
            enemy.Construct(stateMachine);
        }

        private static void ConstructHealthBar(EnemyStaticData enemyData, Enemy enemy)
        {
            EnemyHealth health = enemy.GetComponentInChildren<EnemyHealth>();
            health.Construct(enemyData.Health);

            enemy.GetComponent<ActorUI>()
                .Construct(health);
        }

        private static void TryConstructHurtZone(EnemyStaticData enemyData, Enemy enemy)
        {
            EnemyHurtZone enemyHurtZone = enemy.GetComponent<EnemyHurtZone>();

            if (enemyHurtZone != null)
                enemyHurtZone.Construct(enemyData.Damage, enemyData.AttackRate);
        }
    }
}