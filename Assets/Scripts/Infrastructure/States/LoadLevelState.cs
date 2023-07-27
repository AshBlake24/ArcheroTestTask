using Source.Camera;
using Source.Gameplay;
using Source.Infrastructure.Factories;
using UnityEngine;

namespace Source.Infrastructure.States
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private const string GameFieldTag = "GameField";

        private readonly GameStateMachine _gameStateMachine;
        private readonly LoadingScreen _loadingScreen;
        private readonly SceneLoader _sceneLoader;
        private readonly IGameFactory _gameFactory;

        public LoadLevelState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, LoadingScreen loadingScreen, 
            IGameFactory gameFactory)
        {
            _gameStateMachine = gameStateMachine;
            _loadingScreen = loadingScreen;
            _gameFactory = gameFactory;
            _sceneLoader = sceneLoader;
        }

        public void Enter(string sceneName)
        {
            _loadingScreen.Show();
            _sceneLoader.Load(sceneName, OnLoaded);
        }

        public void Exit()
        {
            _loadingScreen.Hide();
        }

        private void OnLoaded()
        {
            GameField gameField = GetGameField();
            GameObject player = _gameFactory.CreatePlayer(
                gameField.GetComponentInChildren<InitialPoint>().gameObject);
            
            _gameFactory.CreateHud();
            _gameFactory.CreateEnemySpawner(gameField); 
            
            CameraFollow(player.transform);

            _gameStateMachine.Enter<GameLoopState>();
        }

        private void CameraFollow(Transform target)
        {
            UnityEngine.Camera.main
                .GetComponent<CameraFollow>()
                .SetTarget(target);
        }

        private static GameField GetGameField() =>
            GameObject.FindWithTag(GameFieldTag).GetComponent<GameField>();
    }
}