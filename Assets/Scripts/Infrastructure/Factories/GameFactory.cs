using Source.Enemies;
using Source.Enemies.Factories;
using Source.Gameplay;
using Source.Infrastructure.Assets;
using Source.Infrastructure.Services.Input;
using Source.Infrastructure.Services.StaticData;
using Source.Logic;
using Source.Player.Components;
using Source.Player.Data;
using Source.UI;
using UnityEngine;

namespace Source.Infrastructure.Factories
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssetProvider _assetProvider;
        private readonly IEnemyFactory _enemyFactory;
        private readonly IInputService _inputService;
        private readonly IStaticDataService _staticDataService;
        private readonly ICoroutineRunner _coroutineRunner;
        private GameObject _player;

        public GameFactory(IAssetProvider assetProvider, IEnemyFactory enemyFactory, IInputService inputService,
            IStaticDataService staticDataService, ICoroutineRunner coroutineRunner)
        {
            _assetProvider = assetProvider;
            _enemyFactory = enemyFactory;
            _inputService = inputService;
            _staticDataService = staticDataService;
            _coroutineRunner = coroutineRunner;
        }

        public void CreateHud() => _assetProvider.Instantiate(AssetPath.HudPath);

        public GameObject CreatePlayer(GameObject initialPoint)
        {
            _player = _assetProvider.Instantiate(AssetPath.PlayerPath, initialPoint.transform.position);
            PlayerStaticData playerData = _staticDataService.Player;

            ConstructHealthBar(playerData);
            ConstructPlayerComponents(playerData);

            return _player;
        }

        public void CreateEnemySpawner(GameField gameField)
        {
            EnemySpawner spawner = _assetProvider.Instantiate(AssetPath.EnemySpawner)
                .GetComponent<EnemySpawner>();
            
            spawner.Construct(_enemyFactory, _player.transform, gameField);
        }

        public Timer CreateGameStartTimer() => 
            new Timer(_coroutineRunner, _staticDataService.GameConfig.SecondsBeforeGameStart);

        private void ConstructHealthBar(PlayerStaticData playerData)
        {
            IHealth health = _player.GetComponentInChildren<IHealth>();
            health.Construct(playerData.Health);

            _player.GetComponent<ActorUI>()
                .Construct(health);
        }

        private void ConstructPlayerComponents(PlayerStaticData playerData)
        {
            _player.GetComponent<PlayerMovement>()
                .Construct(_inputService, playerData.MovementSpeed);

            _player.GetComponent<PlayerAim>()
                .Construct(_inputService);

            _player.GetComponent<PlayerShooter>()
                .Construct(_inputService, playerData.AttackRate, playerData.AttackForce, playerData.Damage, playerData.ProjectilePrefab);
        }
    }
}