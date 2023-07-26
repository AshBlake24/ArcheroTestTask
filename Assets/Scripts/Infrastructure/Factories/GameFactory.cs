using Source.Enemies;
using Source.Enemies.Factories;
using Source.Infrastructure.Assets;
using UnityEngine;

namespace Source.Infrastructure.Factories
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssetProvider _assetProvider;
        private readonly IEnemyFactory _enemyFactory;
        private GameObject _player;

        public GameFactory(IAssetProvider assetProvider, IEnemyFactory enemyFactory)
        {
            _assetProvider = assetProvider;
            _enemyFactory = enemyFactory;
        }

        public void CreateHud() => _assetProvider.Instantiate(AssetPath.HudPath);

        public GameObject CreatePlayer(GameObject initialPoint)
        {
            _player = _assetProvider.Instantiate(AssetPath.PlayerPath, initialPoint.transform.position);
            return _player;
        }

        public void CreateEnemySpawner()
        {
            EnemySpawner spawner = _assetProvider.Instantiate(AssetPath.EnemySpawner)
                .GetComponent<EnemySpawner>();
            
            spawner.Construct(_enemyFactory, _player.transform);
        }
    }
}