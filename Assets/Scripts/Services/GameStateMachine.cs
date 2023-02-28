using System;
using System.Collections.Generic;
using Services;
using Zenject;

public class GameStateMachine : IGameStateMachine
{
    private Dictionary<Type, IState> _states;

    [Inject]
    public GameStateMachine(DiContainer diContainer)
    {
        _states = new Dictionary<Type, IState>
        {
            [typeof(BootstrapState)] = new BootstrapState(diContainer.Resolve<ISceneLoader>(), this),
            [typeof(LoadProgressState)] = new LoadProgressState(diContainer.Resolve<IProgressService>(), this),
            [typeof(LoadLevelState)] = new LoadLevelState(diContainer.Resolve<IProgressService>(),
                diContainer.Resolve<ISceneLoader>(), this)
        };
    }

    public void Enter<T>() where T : IState
    {
        _states[typeof(T)].Enter();
    }
}