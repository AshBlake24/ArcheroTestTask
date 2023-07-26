namespace Source.Behaviour.States
{
    public interface IState
    {
        void Tick();
        void OnEnter();
        void OnExit();
    }
}