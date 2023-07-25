using Source.Infrastructure.Services.Input;

namespace Source.Infrastructure
{
    public class Game
    {
        public static IInputService InputService { get; private set; }

        public Game()
        {
            RegisterInputService();
        }

        private void RegisterInputService() => 
            InputService = new MobileInputService();
    }
}