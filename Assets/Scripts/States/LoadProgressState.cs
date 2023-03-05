using Cysharp.Threading.Tasks;
using GamePlay.Configs;
using Infrasctructure.Extensions;
using Services;

public class LoadProgressState : IState
{
    private readonly IProgressService _gameProgress;
    private readonly ISaveLoadService _saveLoadService;
    private readonly IGameStateMachine _stateMachine;
    private readonly IConfigService _configService;

    public LoadProgressState(IProgressService gameProgress, ISaveLoadService saveLoadService,
        IGameStateMachine stateMachine, IConfigService configService)
    {
        _gameProgress = gameProgress;
        _saveLoadService = saveLoadService;
        _stateMachine = stateMachine;
        _configService = configService;
    }

    public void Enter()
    {
        LoadProgress().Forget();
    }

    private async UniTaskVoid LoadProgress()
    {
        await LoadGame();
        _stateMachine.Enter<LoadLevelState>();
    }

    private async UniTask LoadGame()
    {
        _gameProgress.GameProgress = _saveLoadService.LoadProgress() ?? await ReturnNewProgress();
    }

    private async UniTask<GameProgress> ReturnNewProgress()
    {
        GameProgress gameProgress = new GameProgress();
        PlayerConfig playerConfig = await _configService.ForPlayer();
        WorldConfig worldConfig = await _configService.ForWorld();

        gameProgress.LootProgress.LootCount = 0;
        gameProgress.PlayerProgress.Damage = playerConfig.Damage;
        gameProgress.PlayerProgress.MaxHp = playerConfig.MaxHealth;
        gameProgress.PlayerProgress.CurrentHp = playerConfig.MaxHealth;
        gameProgress.WorldProgress.PositionOnScene = worldConfig.InitialPosition.ToSerializedVector();
        gameProgress.WorldProgress.SceneToLoadName = worldConfig.InitialLevelName;

        return gameProgress;
    }
}