using System;
using Configs;
using Cysharp.Threading.Tasks;
using GamePlay.HUD;
using Services;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevelState : IState
{
    private readonly IProgressService _progressService;
    private readonly ISceneLoader _sceneLoader;
    private readonly IGameStateMachine _gameStateMachine;
    private readonly IConfigService _configService;
    private IGameFactory _factory;
    private IUIFactory _uiFactory;

    public LoadLevelState(IProgressService progressService, ISceneLoader sceneLoader, IGameFactory factory,
        IGameStateMachine gameStateMachine, IConfigService configService)
    {
        _progressService = progressService;
        _sceneLoader = sceneLoader;
        _gameStateMachine = gameStateMachine;
        _configService = configService;
        _factory = factory;
    }

    public void Enter()
    {
        _factory.CleanUp();
        _factory.Warmup();
        _sceneLoader.LoadScene(_progressService.GameProgress.WorldProgress.SceneToLoadName, OnLoaded);
    }

    private async void OnLoaded()
    {
        await InitUIRoot();
        await InitWorld();
        InformProgressReaders();

        _gameStateMachine.Enter<GameLoopState>();
    }
    
    private async UniTask InitUIRoot()
    {
        await _uiFactory.CreateUIRoot();
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
        LevelConfig levelConfig = await _configService.ForSpawners(SceneManager.GetActiveScene().name);
        foreach (EnemySpawner enemySpawner in levelConfig.EnemySpawner)
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

    private void FollowCamera(GameObject hero)
    {
        if (Camera.main != null)
            Camera.main.GetComponent<CameraFollow>().Follow(hero);
    }
}