using Cysharp.Threading.Tasks;
using Services;
using UnityEngine;

public class UIFactory : IUIFactory
{
    private readonly IAssetProvider _assets;
    private readonly IConfigService _staticData;
    private readonly IProgressService _progressService;
    private readonly IGameStateMachine _gameStateMachine;

    private Transform _uiRoot;

    public UIFactory(IAssetProvider assets, IConfigService staticData,
        IProgressService progressService, IGameStateMachine gameStateMachine)
    {
        _assets = assets;
        _staticData = staticData;
        _progressService = progressService;
        _gameStateMachine = gameStateMachine;
    }
    
    public async UniTask CreateUIRoot()
    {
        GameObject instantiate = await _assets.Instantiate(AssetsAddress.UIRootPath);
        _uiRoot = instantiate.transform;
    }

    public async UniTask CreateLoseGameMenu()
    {
        WindowParameters config = await _staticData.ForWindow(WindowId.LoseGame);
        GameObject window = Object.Instantiate(config.WindowPrefab, _uiRoot);
        window.GetComponent<LoseGameWindow>().Construct(_progressService, _gameStateMachine);
    }

    public async UniTask CreateWinGameMenu()
    {
        WindowParameters config = await _staticData.ForWindow(WindowId.WinGame);
        GameObject window = Object.Instantiate(config.WindowPrefab, _uiRoot);
        window.GetComponent<WinGameWindow>().Construct(_progressService);
    }
}