using Source.Data;
using Source.Data.Service;
using Source.Gameplay;
using Source.Infrastructure.Services.SaveLoadService;
using Source.Infrastructure.Services.StaticData;

namespace Source.Infrastructure.States
{
    public class LoadProgressState : IState
    {
        
        
        private readonly IGameStateMachine _gameStateMachine;
        private readonly IPersistentDataService _persistentDataService;
        private readonly IStaticDataService _staticDataService;
        private readonly ISaveLoadService _saveLoadService;

        public LoadProgressState(IGameStateMachine gameStateMachine, IPersistentDataService persistentDataService,
            IStaticDataService staticDataService, ISaveLoadService saveLoadService)
        {
            _gameStateMachine = gameStateMachine;
            _persistentDataService = persistentDataService;
            _staticDataService = staticDataService;
            _saveLoadService = saveLoadService;
        }

        public void Enter() => 
            LoadProgress();

        public void Exit() { }

        private void LoadProgress()
        {
            _persistentDataService.PlayerProgress =
                _saveLoadService.LoadProgress()
                ?? NewProgress();
            
            _gameStateMachine.Enter<LoadLevelState, string>(GameConfig.LevelName);
        }

        private PlayerProgress NewProgress()
        {
            PlayerProgress progress = new PlayerProgress();

            progress.State.MaxHealth = _staticDataService.Player.Health;
            progress.State.Reset();

            return progress;
        }
    }
}