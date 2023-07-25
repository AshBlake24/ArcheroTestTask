using Source.Infrastructure.States;
using UnityEngine;

namespace Source.Infrastructure
{
    public class Bootstrapper : MonoBehaviour, ICoroutineRunner
    {
        [SerializeField] private LoadingScreen _loadingScreen;
        
        private Game _game;

        private void Awake()
        {
            LoadingScreen loadingScreen = Instantiate(_loadingScreen);
            
            _game = new Game(this, loadingScreen);
            _game.StateMachine.Enter<BootstrapState>();
            
            DontDestroyOnLoad(this);
        }
    }
}
