using Source.Infrastructure.Services;
using Source.Infrastructure.States;

namespace Source.Infrastructure
{
    public class Game
    {
        public readonly GameStateMachine StateMachine;

        public Game(ICoroutineRunner coroutineRunner, LoadingScreen loadingScreen)
        {
            StateMachine = new GameStateMachine(new SceneLoader(coroutineRunner), loadingScreen, ServiceLocator.Container);
        }
    }
}