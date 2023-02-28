public class BootstrapState : IState
{
    private const string BootStrapSceneName = "BootstrapScene";
    private ISceneLoader _sceneLoader;
    private IGameStateMachine _gameStateMachine;
    
    public BootstrapState(ISceneLoader sceneLoader, GameStateMachine gameStateMachine)
    {
        _sceneLoader = sceneLoader;
        _gameStateMachine = gameStateMachine;
    }

    public void Enter()
    {
        _sceneLoader.LoadScene(BootStrapSceneName, EnterLoadProgressState);
    }

    private void EnterLoadProgressState()
    {
        _gameStateMachine.Enter<LoadProgressState>();
    }
}