using Source.Infrastructure.Events;

namespace Source.Gameplay
{
    public interface IGameOverHandler : IGlobalSubscriber
    {
        void OnGameOver();
    }
}