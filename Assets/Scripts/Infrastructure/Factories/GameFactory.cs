using Source.Enemies;
using Source.Enemies.Factories;
using Source.Gameplay;
using Source.Infrastructure.Assets;
using Source.Infrastructure.Services;
using Source.Infrastructure.Services.Input;
using Source.Infrastructure.Services.SaveLoadService;
using Source.Infrastructure.Services.StaticData;
using Source.Infrastructure.States;
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
        private readonly ISaveLoadService _saveLoadService;
        private readonly IGameStateMachine _gameStateMachine;
        private GameObject _player;

        public GameFactory(IAssetProvider assetProvider, IEnemyFactory enemyFactory, IInputService inputService,
            IStaticDataService staticDataService, ISaveLoadService saveLoadService, IGameStateMachine gameStateMachine,
            ICoroutineRunner coroutineRunner)
        {
            _assetProvider = assetProvider;
            _enemyFactory = enemyFactory;
            _inputService = inputService;
            _staticDataService = staticDataService;
            _coroutineRunner = coroutineRunner;
            _saveLoadService = saveLoadService;
            _gameStateMachine = gameStateMachine;
        }

        public void CreateHud() => _assetProvider.InstantiateRegistered(AssetPath.HudPath);

        public GameObject CreatePlayer(GameObject initialPoint)
        {
            _player = _assetProvider.InstantiateRegistered(AssetPath.PlayerPath, initialPoint.transform.position);
            PlayerStaticData playerData = _staticDataService.Player;

            ConstructPlayerComponents(playerData);

            return _player;
        }

        public void CreateEnemySpawner(GameField gameField)
        {
            EnemySpawner spawner = _assetProvider.Instantiate(AssetPath.EnemySpawner)
                .GetComponent<EnemySpawner>();
            
            spawner.Construct(_enemyFactory, _player, gameField);
        }

        public Timer CreateGameStartTimer() => 
            new Timer(_coroutineRunner, _staticDataService.GameConfig.SecondsBeforeGameStart);

        public void CreateGameRestarter() =>
            _assetProvider.Instantiate(AssetPath.GameRestarterPath)
                .GetComponent<GameRestarter>()
                .Construct(_gameStateMachine, _staticDataService, _saveLoadService, _player.GetComponent<PlayerDeath>());

        private void ConstructPlayerComponents(PlayerStaticData playerData)
        {
            _player.GetComponent<PlayerMovement>()
                .Construct(_inputService, playerData.MovementSpeed);

            _player.GetComponent<PlayerAim>()
                .Construct(_inputService);

            _player.GetComponent<PlayerShooter>()
                .Construct(_inputService, playerData.AttackRate, playerData.AttackForce, playerData.Damage, playerData.ProjectilePrefab);
            
            _player.GetComponent<ActorUI>()
                .Construct(_player.GetComponentInChildren<PlayerHealth>());
        }
    }
}