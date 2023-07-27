using System;
using System.Collections.Generic;
using Source.Data.Service;
using Source.Infrastructure.Factories;
using Source.Infrastructure.Services;
using Source.Infrastructure.Services.SaveLoadService;
using Source.Infrastructure.Services.StaticData;
using Source.UI.Factory;

namespace Source.Infrastructure.States
{
    public class GameStateMachine : IGameStateMachine
    {
        private readonly Dictionary<Type, IExitableState> _states;
        private IExitableState _activeState;

        public GameStateMachine(SceneLoader sceneLoader, LoadingScreen loadingScreen, ICoroutineRunner coroutineRunner,
            ServiceLocator services)
        {
            _states = new Dictionary<Type, IExitableState>()
            {
                [typeof(BootstrapState)] = new BootstrapState(this, sceneLoader, coroutineRunner, services),
                [typeof(LoadProgressState)] = new LoadProgressState(this, services.Single<IPersistentDataService>(),
                    services.Single<IStaticDataService>(), services.Single<ISaveLoadService>()),
                
                [typeof(LoadLevelState)] = new LoadLevelState(this, sceneLoader, loadingScreen, 
                    services.Single<IGameFactory>(), services.Single<IUIFactory>(), services.Single<ISaveLoadService>()),
                
                [typeof(GameLoopState)] = new GameLoopState(this),
            };
        }

        public void Enter<TState>() where TState : class, IState
        {
            IState state = ChangeState<TState>();
            state.Enter();
        }

        public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload>
        {
            IPayloadedState<TPayload> state = ChangeState<TState>();
            state.Enter(payload);
        }

        private TState ChangeState<TState>() where TState : class, IExitableState
        {
            _activeState?.Exit();

            TState state = GetState<TState>();
            _activeState = state;

            return state;
        }

        private TState GetState<TState>() where TState : class, IExitableState =>
            _states[typeof(TState)] as TState;
    }
}