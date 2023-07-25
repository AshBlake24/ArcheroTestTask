using Source.Infrastructure.Services.Input;

namespace Source.Infrastructure.States
{
    public class BootstrapState : IState
    {
        private readonly IGameStateMachine _gameStateMachine;

        public BootstrapState(IGameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
            
            RegisterServices();
        }

        public void Enter()
        {
        }

        public void Exit()
        {
        }

        private void RegisterServices()
        {
            Game.s_InputService = RegisterInputService();
        }

        private IInputService RegisterInputService() => 
            new MobileInputService();
    }
}