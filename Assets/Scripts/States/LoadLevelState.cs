using Cysharp.Threading.Tasks;
using Services;
using UnityEngine;

public class LoadLevelState : IState
{
    private readonly IProgressService _progressService;
    private readonly ISceneLoader _sceneLoader;
    private readonly IGameStateMachine _gameStateMachine;
    private IGameFactory _factory;

    public LoadLevelState(IProgressService progressService, ISceneLoader sceneLoader, IGameFactory factory, IGameStateMachine gameStateMachine)
    {
        _progressService = progressService;
        _sceneLoader = sceneLoader;
        _gameStateMachine = gameStateMachine;
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
        Debug.Log("MakeUIRoot");
    }

    private async UniTask InitWorld()
    {
        GameObject hero = await _factory.CreatePlayer();
        FollowCamera(hero);
    }

    private void InformProgressReaders()
    {
        foreach (IProgressHandler progressHandler in _factory.ProgressHandlers)
        {
            progressHandler.LoadProgress();
        }
    }

    private void FollowCamera(GameObject hero)
    {
        if (Camera.main != null)
            Camera.main.GetComponent<CameraFollow>().Follow(hero);
    }
}