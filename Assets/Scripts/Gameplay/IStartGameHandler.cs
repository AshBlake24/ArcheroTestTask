using Source.Infrastructure.Events;

namespace Source.Gameplay
{
    public interface IStartGameHandler : IGlobalSubscriber
    {
        void OnGameStarted();
    }
}