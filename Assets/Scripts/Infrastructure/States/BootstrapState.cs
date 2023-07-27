using Source.Enemies.Factories;
using Source.Infrastructure.Assets;
using Source.Infrastructure.Factories;
using Source.Infrastructure.Services;
using Source.Infrastructure.Services.Input;
using Source.Infrastructure.Services.StaticData;
using Source.UI.Factory;

namespace Source.Infrastructure.States
{
    public class BootstrapState : IState
    {
        private const string InitialScene = "Initial";
        
        private readonly IGameStateMachine _gameStateMachine;
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly SceneLoader _sceneLoader;
        private readonly ServiceLocator _services;

        public BootstrapState(IGameStateMachine gameStateMachine, SceneLoader sceneLoader, ICoroutineRunner coroutineRunner,
            ServiceLocator services)
        {
            _gameStateMachine = gameStateMachine;
            _coroutineRunner = coroutineRunner;
            _sceneLoader = sceneLoader;
            _services = services;

            RegisterServices();
        }

        public void Enter()
        {
            _sceneLoader.Load(InitialScene, onLoaded: EnterLoadLevel);
        }

        public void Exit()
        {
        }

        private void EnterLoadLevel() => 
            _gameStateMachine.Enter<LoadLevelState, string>("Game");

        private void RegisterServices()
        {
            RegisterStaticDataService();
            _services.RegisterSingle<IGameStateMachine>(_gameStateMachine);
            _services.RegisterSingle<IInputService>(GetInputService());
            _services.RegisterSingle<IAssetProvider>(new AssetProvider());
            
            _services.RegisterSingle<IEnemyBehaviourFactory>(new EnemyBehaviourFactory());
            
            _services.RegisterSingle<IUIFactory>(new UIFactory(
                _services.Single<IAssetProvider>()));
            
            _services.RegisterSingle<IEnemyFactory>(new EnemyFactory(
                _services.Single<IStaticDataService>(),
                _services.Single<IEnemyBehaviourFactory>()));
            
            _services.RegisterSingle<IGameFactory>(new GameFactory(
                _services.Single<IAssetProvider>(),
                _services.Single<IEnemyFactory>(),
                _services.Single<IInputService>(),
                _services.Single<IStaticDataService>(),
                _coroutineRunner));
        }

        private void RegisterStaticDataService()
        {
            IStaticDataService staticDataService = new StaticDataService();
            staticDataService.Load();
            _services.RegisterSingle(staticDataService);
        }

        private IInputService GetInputService() => 
            new MobileInputService();
    }
}