namespace Source.Enemies.Factory
{
    public interface IEnemyFactory
    {
        Enemy CreateEnemy(EnemyId id);
    }
}