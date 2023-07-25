using Source.Infrastructure.Services.Input;
using Source.Infrastructure.States;

namespace Source.Infrastructure
{
    public class Game
    {
        public static IInputService s_InputService;
        public GameStateMachine StateMachine;

        public Game()
        {
            StateMachine = new GameStateMachine();
        }
    }
}