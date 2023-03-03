using Services;

public class LoadProgressState : IState
{
    private readonly IProgressService _gameProgress;
    private readonly ISaveLoadService _saveLoadService;
    private readonly IGameStateMachine _stateMachine;

    public LoadProgressState(IProgressService gameProgress, ISaveLoadService saveLoadService,
        IGameStateMachine stateMachine)
    {
        _gameProgress = gameProgress;
        _saveLoadService = saveLoadService;
        _stateMachine = stateMachine;
    }

    public void Enter()
    {
        LoadGame();
        _stateMachine.Enter<LoadLevelState>();
    }

    private void LoadGame()
    {
        _gameProgress.GameProgress = _saveLoadService.LoadProgress() ?? new GameProgress();
    }
}