using Source.Infrastructure.States;

namespace Source.Infrastructure.Services
{
    public class SceneLoadingService : ISceneLoadingService
    {
        private readonly IGameStateMachine _gameStateMachine;

        public SceneLoadingService(IGameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }

        public void Load(string level) => 
            _gameStateMachine.Enter<LoadLevelState, string>(level);
    }
}