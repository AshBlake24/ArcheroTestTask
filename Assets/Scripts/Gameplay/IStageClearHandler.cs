using Source.Infrastructure.Events;

namespace Source.Gameplay
{
    public interface IStageClearHandler : IGlobalSubscriber
    {
        void OnStageCleared();
    }
}