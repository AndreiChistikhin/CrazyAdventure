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

        public GameStateMachine(DiContainer diContainer)
        {
            ISceneLoader sceneLoader = diContainer.Resolve<ISceneLoader>();
            ISaveLoadService saveLoadService = diContainer.Resolve<ISaveLoadService>();
            IProgressService progressService = diContainer.Resolve<IProgressService>();
            IConfigService configService = diContainer.Resolve<IConfigService>();
            IGameFactory gameFactory = diContainer.Resolve<IGameFactory>();
            IUIFactory uiFactory = diContainer.Resolve<IUIFactory>();

            _states = new Dictionary<Type, IState>
            {
                [typeof(BootstrapState)] = new BootstrapState(sceneLoader, this),
                [typeof(LoadProgressState)] =
                    new LoadProgressState(progressService, saveLoadService, configService, this),
                [typeof(LoadLevelState)] = new LoadLevelState(progressService, sceneLoader, gameFactory, configService, uiFactory, this),
                [typeof(GameLoopState)] = new GameLoopState()
            };
        }

        public void Enter<T>() where T : IState
        {
            _states[typeof(T)].Enter();
        }
    }
}