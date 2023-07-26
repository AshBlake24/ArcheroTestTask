using System;
using Source.Behaviour;
using Source.Behaviour.States;
using Source.Enemies;
using Source.Infrastructure.Assets;
using UnityEngine;
using UnityEngine.AI;

namespace Source.Infrastructure.Factories
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssetProvider _assetProvider;
        private Func<bool> TargetReached;

        public GameFactory(IAssetProvider assetProvider)
        {
            _assetProvider = assetProvider;
        }

        public void CreateHud() => _assetProvider.Instantiate(AssetPath.HudPath);

        public GameObject CreatePlayer(GameObject initialPoint) => 
            _assetProvider.Instantiate(AssetPath.PlayerPath, initialPoint.transform.position);

        public void CreateEnemy(Transform target)
        {
            
        }
    }
}