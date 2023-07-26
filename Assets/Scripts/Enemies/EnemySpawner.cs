using System.Collections.Generic;
using Source.Enemies.Data;
using Source.Enemies.Factories;
using UnityEngine;

namespace Source.Enemies
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private List<Transform> _spawnPoints;
        
        private IEnemyFactory _enemyFactory;
        private Transform _target;

        public void Construct(IEnemyFactory enemyFactory, Transform player)
        {
            _enemyFactory = enemyFactory;
            _target = player;
        }

        private void Start()
        {
            SpawnEnemy();
        }

        private void SpawnEnemy() => 
            _enemyFactory.CreateEnemy(EnemyId.Bat, transform, _target);
    }
}