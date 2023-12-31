﻿using Source.Camera;
using Source.Gameplay;
using Source.Infrastructure.Factories;
using Source.Infrastructure.Services.SaveLoadService;
using Source.Logic;
using Source.UI.Factory;
using UnityEngine;

namespace Source.Infrastructure.States
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private const string GameFieldTag = "GameField";

        private readonly GameStateMachine _gameStateMachine;
        private readonly LoadingScreen _loadingScreen;
        private readonly SceneLoader _sceneLoader;
        private readonly IGameFactory _gameFactory;
        private readonly IUIFactory _uiFactory;
        private readonly ISaveLoadService _saveLoadService;

        public LoadLevelState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, LoadingScreen loadingScreen, 
            IGameFactory gameFactory, IUIFactory uiFactory, ISaveLoadService saveLoadService)
        {
            _gameStateMachine = gameStateMachine;
            _loadingScreen = loadingScreen;
            _gameFactory = gameFactory;
            _uiFactory = uiFactory;
            _saveLoadService = saveLoadService;
            _sceneLoader = sceneLoader;
        }

        public void Enter(string sceneName)
        {
            _loadingScreen.Show();
            _sceneLoader.Load(sceneName, OnLoaded);
        }

        public void Exit()
        {
            _loadingScreen.Hide();
        }

        private void OnLoaded()
        {
            InitUIRoot();
            InitGameWorld();
            InformProgressReaders();

            _gameStateMachine.Enter<GameLoopState>();
        }

        private void InitUIRoot() =>
            _uiFactory.CreateUIRoot();

        private void InitGameWorld()
        {
            GameField gameField = GetGameField();
            GameObject player = _gameFactory.CreatePlayer(
                gameField.GetComponentInChildren<InitialPoint>().gameObject);
            Timer gameStartTimer = _gameFactory.CreateGameStartTimer();

            _gameFactory.CreateHud();
            _gameFactory.CreateEnemySpawner(gameField);
            _gameFactory.CreateGameRestarter();
            _uiFactory.CreateCountdownTimer(gameStartTimer);
            
            CameraFollow(player.transform);
            
            gameStartTimer.Start();
        }

        private void CameraFollow(Transform target)
        {
            UnityEngine.Camera.main
                .GetComponent<CameraFollow>()
                .SetTarget(target);
        }
        
        private void InformProgressReaders() =>
            _saveLoadService.InformProgressReaders();
        
        private void Cleanup() => 
            _saveLoadService.Cleanup();

        private static GameField GetGameField() =>
            GameObject.FindWithTag(GameFieldTag).GetComponent<GameField>();
    }
}