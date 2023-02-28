using Infrasctructure.Extensions;
using Services;
using UnityEngine;

public class LoadProgressState : IState
{
    private const string SaveInfoKey = "SaveStats";

    private readonly IProgressService _gameProgress;
    private readonly IGameStateMachine _stateMachine;

    public LoadProgressState(IProgressService gameProgress, IGameStateMachine stateMachine)
    {
        _gameProgress = gameProgress;
        _stateMachine = stateMachine;
    }

    public void Enter()
    {
        LoadGame();
        _stateMachine.Enter<LoadLevelState>();
    }

    private void LoadGame()
    {
        _gameProgress.GameProgress = PlayerPrefs.GetString(SaveInfoKey).Deserialize<GameProgress>() ?? new GameProgress();
    }
}

public class LoadLevelState : IState
{
    private readonly IProgressService _progressService;
    private readonly ISceneLoader _sceneLoader;
    private readonly IGameStateMachine _gameStateMachine;

    public LoadLevelState(IProgressService progressService, ISceneLoader sceneLoader, IGameStateMachine gameStateMachine)
    {
        _progressService = progressService;
        _sceneLoader = sceneLoader;
        _gameStateMachine = gameStateMachine;
    }

    //TODO замутить инициализацию первоначальной сцены в сохранениях
    public void Enter()
    {
        _sceneLoader.LoadScene(_progressService.GameProgress.WorldProgress.CurrentSceneName, InitWorld);
    }

    private void InitWorld()
    {
        //TODO: await Init
        _gameStateMachine.Enter<GameLoopState>();
    }
}

internal class GameLoopState : IState
{
    public void Enter()
    {
    }
}