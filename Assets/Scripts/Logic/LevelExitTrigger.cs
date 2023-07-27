using Source.Infrastructure.Services;
using Source.Player.Components;
using UnityEngine;

namespace Source.Logic
{
    public class LevelExitTrigger : MonoBehaviour
    {
        private const string PlayerTag = "Player";
        
        [SerializeField] private string _levelName;

        private ISceneLoadingService _sceneLoadingService;
        private bool _isTriggered;

        private void Awake() => 
            _sceneLoadingService = ServiceLocator.Container.Single<ISceneLoadingService>();

        private void OnTriggerEnter(Collider other)
        {
            if (_isTriggered)
                return;

            PlayerHealth player = other.GetComponentInChildren<PlayerHealth>();

            if (player != null)
            {
                _isTriggered = true;
                _sceneLoadingService.Load(_levelName);
            }
        }
    }
}