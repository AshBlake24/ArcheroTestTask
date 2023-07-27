using Source.Enemies.Factories;
using Source.Gameplay;
using Source.Infrastructure.Events;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Source.Enemies
{
    public class EnemySpawner : MonoBehaviour
    {
        private readonly Collider[] _obstacleHits = new Collider[3];
        private readonly Collider[] _enemiesHits = new Collider[1];
        
        private static int s_aliveEnemies;

        [SerializeField] private LayerMask _obstaclesLayerMask;
        [SerializeField] private LayerMask _enemiesLayerMask;
        [SerializeField] private float _distanceToObstacle;
        [SerializeField] private float _distanceToEnemies;
        [SerializeField, Min(0)] private int _minEnemiesCount;
        [SerializeField, Min(0)] private int _maxEnemiesCount;
        [SerializeField, Min(0)] private int _attemptsToGetPoint;
        
        private IEnemyFactory _enemyFactory;
        private GameField _gameField;
        private Transform _target;
        private Bounds _spawnAreaBounds;

        public void Construct(IEnemyFactory enemyFactory, Transform player, GameField gameField)
        {
            _enemyFactory = enemyFactory;
            _gameField = gameField;
            _target = player;
            
            CalculateSpawnArea();
            SpawnRandomEnemies();
        }

        private void SpawnRandomEnemies()
        {
            int enemiesCount = Random.Range(_minEnemiesCount, _maxEnemiesCount + 1);

            for (int i = 0; i < enemiesCount; i++)
            {
                if (TryGetValidPoint(out Vector3 spawnPoint)) 
                    SpawnRandomEnemy(spawnPoint, _target, transform);
            }
        }

        private void SpawnRandomEnemy(Vector3 spawnPoint, Transform target, Transform parent)
        {
            Enemy enemy = _enemyFactory.CreateRandomEnemy(spawnPoint, target, parent)
                ;
            EnemyDeath enemyDeath = enemy.GetComponent<EnemyDeath>();
            enemyDeath.Died += OnEnemyDied;

            s_aliveEnemies++;
        }

        private void CalculateSpawnArea()
        {
            Bounds bounds = _gameField.Bounds;

            float minPointALongZ = Mathf.Abs(bounds.min.z / 3);
            float maxPointALongZ = Mathf.Abs(bounds.max.z);

            Vector3 center = new Vector3(bounds.center.x, bounds.center.y, maxPointALongZ - (minPointALongZ * 2));
            Vector3 size = new Vector3(bounds.size.x, 0, maxPointALongZ + minPointALongZ);

            _spawnAreaBounds = new Bounds(center, size);
        }

        private bool TryGetValidPoint(out Vector3 point)
        {
            point = Vector3.zero;
            
            for (int i = 0; i < _attemptsToGetPoint; i++)
            {
                point = GetRandomSpawnPoint();
                int obstacleHits = Physics.OverlapSphereNonAlloc(point, _distanceToObstacle, _obstacleHits, _obstaclesLayerMask);
                int enemiesHits = Physics.OverlapSphereNonAlloc(point, _distanceToEnemies, _enemiesHits, _enemiesLayerMask);

                if (obstacleHits == 1 && enemiesHits == 0)
                    return true;
            }

            return false;
        }

        private Vector3 GetRandomSpawnPoint() => new Vector3(
            Random.Range(_spawnAreaBounds.min.x, _spawnAreaBounds.max.x),
            Random.Range(_spawnAreaBounds.min.y, _spawnAreaBounds.max.y),
            Random.Range(_spawnAreaBounds.min.z, _spawnAreaBounds.max.z));

        private void OnEnemyDied(EnemyDeath enemy)
        {
            enemy.Died -= OnEnemyDied;
            
            s_aliveEnemies--;
            
            if (s_aliveEnemies <= 0)
                EventBus.RaiseEvent<IStageClearHandler>(h => h.OnStageCleared());
        }
    }
}