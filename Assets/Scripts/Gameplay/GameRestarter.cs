using System.Collections;
using Source.Infrastructure.Events;
using Source.Infrastructure.Services;
using Source.Infrastructure.Services.StaticData;
using Source.Player.Components;
using Source.Utilities;
using UnityEngine;

namespace Source.Gameplay
{
    public class GameRestarter : MonoBehaviour
    {
        private const string LevelName = "Game";
        
        private ISceneLoadingService _sceneLoadingService;
        private IStaticDataService _staticDataService;
        private PlayerDeath _playerDeath;

        public void Construct(ISceneLoadingService sceneLoadingService, IStaticDataService staticDataService, 
            PlayerDeath playerDeath)
        {
            _sceneLoadingService = sceneLoadingService;
            _staticDataService = staticDataService;
            _playerDeath = playerDeath;

            _playerDeath.Died += OnPlayerDied;
        }

        private void OnPlayerDied()
        {
            _playerDeath.Died -= OnPlayerDied;
            
            EventBus.RaiseEvent<IGameOverHandler>(h => h.OnGameOver());
            
            StartCoroutine(RestartLevel());
        }

        private IEnumerator RestartLevel()
        {
            yield return TimeUtility.GetTime(_staticDataService.GameConfig.TimeToRestart);
            
            _sceneLoadingService.Load(LevelName);
        }
    }
}