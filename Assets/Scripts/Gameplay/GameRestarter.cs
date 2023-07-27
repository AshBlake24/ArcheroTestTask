using System.Collections;
using Source.Infrastructure.Events;
using Source.Infrastructure.Services;
using Source.Infrastructure.Services.SaveLoadService;
using Source.Infrastructure.Services.StaticData;
using Source.Infrastructure.States;
using Source.Player.Components;
using Source.Utilities;
using UnityEngine;

namespace Source.Gameplay
{
    public class GameRestarter : MonoBehaviour
    {
        private IGameStateMachine _gameStateMachine;
        private IStaticDataService _staticDataService;
        private ISaveLoadService _saveLoadService;
        private PlayerDeath _playerDeath;

        public void Construct(IGameStateMachine gameStateMachine, IStaticDataService staticDataService, 
            ISaveLoadService saveLoadService, PlayerDeath playerDeath)
        {
            _gameStateMachine = gameStateMachine;
            _staticDataService = staticDataService;
            _saveLoadService = saveLoadService;
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
            
            _saveLoadService.ClearProgress();
            _gameStateMachine.Enter<LoadProgressState>();
        }
    }
}