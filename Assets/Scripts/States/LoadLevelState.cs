using System;
using System.Threading.Tasks;
using Configs;
using Cysharp.Threading.Tasks;
using GamePlay;
using GamePlay.HUD;
using Progress;
using Services;
using Services.Interfaces;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace States
{
    public class LoadLevelState : IState
    {
        private readonly IProgressService _progressService;
        private readonly ISceneLoader _sceneLoader;
        private readonly IGameStateMachine _gameStateMachine;
        private readonly IConfigService _configService;
        private IGameFactory _factory;
        private IUIFactory _uiFactory;
        private IServerRequester _serverRequester;
        private ITimeChecker _timeChecker;

        public LoadLevelState(IProgressService progressService, ISceneLoader sceneLoader, IGameFactory factory,
            IConfigService configService, IUIFactory uiFactory, IServerRequester serverRequester,
            ITimeChecker timeChecker,
            IGameStateMachine gameStateMachine)
        {
            _timeChecker = timeChecker;
            _serverRequester = serverRequester;
            _progressService = progressService;
            _sceneLoader = sceneLoader;
            _gameStateMachine = gameStateMachine;
            _configService = configService;
            _factory = factory;
            _uiFactory = uiFactory;
        }

        public void Enter()
        {
            _factory.CleanUp();
            _sceneLoader.LoadScene(_progressService.GameProgress.WorldProgress.SceneToLoadName, OnLoaded);
        }

        private async void OnLoaded()
        {
            await InitUIRoot();
            await CheckAuthorization();
            await InitWorld();
            InformProgressReaders();
            StartTimer();

            _gameStateMachine.Enter<GameLoopState>();
        }

        private async UniTask InitUIRoot()
        {
            await _uiFactory.CreateUIRoot();
        }

        private async UniTask CheckAuthorization()
        {
            if (String.IsNullOrEmpty(_serverRequester.Token))
                await _uiFactory.CreateLoginRegisterWindow();
        }

        private async UniTask InitWorld()
        {
            GameObject hero = await _factory.CreatePlayer();
            FollowCamera(hero);
            await InitHUD(hero);
            await InitSpawners();
        }

        private async UniTask InitHUD(GameObject hero)
        {
            GameObject HUD = await _factory.CreateHUD();
            HUD.GetComponentInChildren<ActorUI>().Construct(hero.GetComponent<IHealth>());
        }

        private async UniTask InitSpawners()
        {
            EnemyPositions enemyPositions = await _configService.ForEnemyPositions(SceneManager.GetActiveScene().name);
            foreach (EnemySpawner enemySpawner in enemyPositions.EnemySpawner)
            {
                if (_progressService.GameProgress.EnemyProgress.ClearedSpawners.Contains(enemySpawner.EnemyId))
                    continue;
                await _factory.CreateEnemy(enemySpawner, enemySpawner.EnemyId);
            }
        }

        private void InformProgressReaders()
        {
            foreach (IProgressHandler progressHandler in _factory.ProgressHandlers)
            {
                progressHandler.LoadProgress(_progressService.GameProgress);
            }
        }

        private void StartTimer()
        {
            _timeChecker.StartTimer();
        }

        private void FollowCamera(GameObject hero)
        {
            if (Camera.main != null)
                Camera.main.GetComponent<CameraFollow>().Follow(hero);
        }
    }
}