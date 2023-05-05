using System;
using System.Collections.Generic;
using Services.Interfaces;
using States;
using Zenject;

namespace Services
{
    public class GameStateMachine : IGameStateMachine
    {
        private Dictionary<Type, IState> _states;

        public GameStateMachine(ISceneLoader sceneLoader, ISaveLoadService saveLoadService,
            IProgressService progressService, IConfigService configService, IGameFactory gameFactory,
            IUIFactory uiFactory, IServerRequester serverRequester)
        {
            _states = new Dictionary<Type, IState>
            {
                [typeof(BootstrapState)] = new BootstrapState(sceneLoader, this),
                [typeof(LoadProgressState)] =
                    new LoadProgressState(progressService, saveLoadService, configService, this),
                [typeof(LoadLevelState)] = new LoadLevelState(progressService, sceneLoader, gameFactory, configService,
                    uiFactory, serverRequester, this),
                [typeof(GameLoopState)] = new GameLoopState()
            };
        }

        public void Enter<T>() where T : IState
        {
            _states[typeof(T)].Enter();
        }
    }
}