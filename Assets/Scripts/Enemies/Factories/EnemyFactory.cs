using System;
using Source.Behaviour;
using Source.Enemies.Data;
using Source.Infrastructure.Services.StaticData;
using UnityEngine;
using Object = UnityEngine.Object;

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

        public Enemy CreateEnemy(EnemyId id, Transform parent, Transform target)
        {
            if (id == EnemyId.Unknown)
                throw new ArgumentNullException(nameof(id), "Enemy id is not specified");

            EnemyStaticData enemyData = _staticDataService.GetDataById<EnemyId, EnemyStaticData>(id);

            return ConstructEnemy(enemyData, parent, target);
        }

        private Enemy ConstructEnemy(EnemyStaticData enemyData, Transform parent, Transform target)
        {
            Enemy enemy = Object.Instantiate(enemyData.Prefab, parent.position, Quaternion.identity, parent)
                .GetComponent<Enemy>();
            
            enemy.GetComponentInChildren<EnemyHealth>()
                .Construct(enemyData.Health);

            StateMachine stateMachine = GetEnemyBehaviour(enemyData, target, enemy);

            enemy.Construct(stateMachine);

            return enemy;
        }

        private StateMachine GetEnemyBehaviour(EnemyStaticData enemyData, Transform target, Enemy enemy) =>
            _enemyBehaviourFactory.CreateEnemyBehaviour(enemyData, target, enemy);
    }
}