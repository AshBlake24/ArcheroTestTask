using Source.Enemies.Factory;

namespace Source.Enemies
{
    public class EnemySpawner
    {
        private readonly IEnemyFactory _enemyFactory;

        public EnemySpawner(IEnemyFactory enemyFactory)
        {
            _enemyFactory = enemyFactory;
        }
        
        public void SpawnEnemy()
        {
            Enemy enemy = _enemyFactory.CreateEnemy(EnemyId.Capsule);
        }
    }
}