namespace Source.Infrastructure.States
{
    public interface IGameStateMachine
    {
        void Enter<TState>() where TState : class, IState;
    }
}