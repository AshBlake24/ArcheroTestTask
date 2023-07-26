using Source.Enemies.Factories;
using Source.Infrastructure.Assets;
using Source.Infrastructure.Factories;
using Source.Infrastructure.Services;
using Source.Infrastructure.Services.Input;
using Source.Infrastructure.Services.StaticData;

namespace Source.Infrastructure.States
{
    public class BootstrapState : IState
    {
        private const string InitialScene = "Initial";
        
        private readonly IGameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly ServiceLocator _services;

        public BootstrapState(IGameStateMachine gameStateMachine, SceneLoader sceneLoader, ServiceLocator services)
        {
            _gameStateMachine = gameStateMachine;
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
            _services.RegisterSingle<IInputService>(GetInputService());
            _services.RegisterSingle<IAssetProvider>(new AssetProvider());
            
            _services.RegisterSingle<IEnemyBehaviourFactory>(new EnemyBehaviourFactory());
            
            _services.RegisterSingle<IEnemyFactory>(new EnemyFactory(
                _services.Single<IStaticDataService>(),
                _services.Single<IEnemyBehaviourFactory>()));
            
            _services.RegisterSingle<IGameFactory>(new GameFactory(
                _services.Single<IAssetProvider>()));
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