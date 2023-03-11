using Services;
using Services.Interfaces;
using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [SerializeField] private LoadingCurtain _loadingCurtain;
    
    public override void InstallBindings()
    {
        BindContainer();
        InstallLoadingObjects();
        InstallServices();
    }
    
    private void BindContainer()
    {
        Container.BindInterfacesAndSelfTo<GameInstaller>().FromInstance(this).AsSingle();
    }

    private void InstallLoadingObjects()
    {
        Container.Bind<LoadingCurtain>().FromInstance(_loadingCurtain).AsSingle();
    }

    private void InstallServices()
    {
        Container.Bind<ISceneLoader>().To<SceneLoader>().AsSingle();
        Container.Bind<IGameStateMachine>().To<GameStateMachine>().AsSingle();
        Container.Bind<IProgressService>().To<ProgressService>().AsSingle();
        Container.Bind<IGameFactory>().To<GameFactory>().AsSingle();
        Container.Bind<ISaveLoadService>().To<SaveLoadService>().AsSingle();
        Container.Bind<IAssetProvider>().To<AssetProvider>().AsSingle();
        Container.Bind<IInputService>().To<InputService>().AsSingle();
        Container.Bind<IConfigService>().To<ConfigService>().AsSingle();
        Container.Bind<IUIFactory>().To<UIFactory>().AsSingle();
        Container.Bind<IWindowService>().To<WindowService>().AsSingle();
    }
}
