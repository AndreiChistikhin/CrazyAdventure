using System;
using System.Collections.Generic;
using Zenject;

public class GameStateMachine : IGameStateMachine
{
    private Dictionary<Type, IState> _states;
    
    [Inject]
    public GameStateMachine(DiContainer diContainer)
    {
        ISceneLoader loader = diContainer.Resolve<ISceneLoader>();
        _states = new Dictionary<Type, IState>
        {
            [typeof(BootstrapState)] = new BootstrapState(loader, this),
            [typeof(LoadProgressState)] = new LoadProgressState()
        };
    }
    
    public void Enter<T>() 
    {
         _states[typeof(T)].Enter();
    }
}