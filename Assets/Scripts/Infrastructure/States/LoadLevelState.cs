using Source.Camera;
using UnityEngine;

namespace Source.Infrastructure.States
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private const string InitialPointTag = "InitialPoint";
        private const string PlayerPath = "Player/Player";
        private const string HudPath = "UI/HUD";

        private readonly GameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;

        public LoadLevelState(GameStateMachine gameStateMachine, SceneLoader sceneLoader)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
        }

        public void Enter(string sceneName)
        {
            _sceneLoader.Load(sceneName, OnLoaded);
        }

        public void Exit()
        {
        }

        private void OnLoaded()
        {
            GameObject initialPoint = GameObject.FindWithTag(InitialPointTag);
            GameObject player = Instantiate(PlayerPath, initialPoint.transform.position);
            Instantiate(HudPath);
            
            CameraFollow(player.transform);
        }

        private static GameObject Instantiate(string path)
        {
            GameObject prefab = Resources.Load<GameObject>(path);
            return Object.Instantiate(prefab);
        }
        
        private static GameObject Instantiate(string path, Vector3 position)
        {
            GameObject prefab = Resources.Load<GameObject>(path);
            return Object.Instantiate(prefab, position, Quaternion.identity);
        }

        private void CameraFollow(Transform target)
        {
            UnityEngine.Camera.main
                .GetComponent<CameraFollow>()
                .SetTarget(target);
        }
    }
}