using Source.Camera;
using Source.Infrastructure.Factories;
using UnityEngine;

namespace Source.Infrastructure.States
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private const string InitialPointTag = "InitialPoint";
        
        private readonly GameStateMachine _gameStateMachine;
        private readonly LoadingScreen _loadingScreen;
        private readonly SceneLoader _sceneLoader;
        private readonly GameFactory _gameFactory;

        public LoadLevelState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, LoadingScreen loadingScreen)
        {
            _gameStateMachine = gameStateMachine;
            _loadingScreen = loadingScreen;
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
            GameObject player = _gameFactory.CreatePlayer(GameObject.FindWithTag(InitialPointTag));
            _gameFactory.CreateHud();
            CameraFollow(player.transform);
            
            _gameStateMachine.Enter<GameLoopState>();
        }

        private void CameraFollow(Transform target)
        {
            UnityEngine.Camera.main
                .GetComponent<CameraFollow>()
                .SetTarget(target);
        }
    }
}