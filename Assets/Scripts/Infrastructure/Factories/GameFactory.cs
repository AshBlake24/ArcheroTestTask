using Source.Infrastructure.Assets;
using UnityEngine;

namespace Source.Infrastructure.Factories
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssetProvider _assetProvider;

        public GameFactory(IAssetProvider assetProvider)
        {
            _assetProvider = assetProvider;
        }

        public void CreateHud() => _assetProvider.Instantiate(AssetPath.HudPath);

        public GameObject CreatePlayer(GameObject initialPoint) => 
            _assetProvider.Instantiate(AssetPath.PlayerPath, initialPoint.transform.position);

        public void CreateEnemySpawner(Transform target)
        {
            
        }
    }
}