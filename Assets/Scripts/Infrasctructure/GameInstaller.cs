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
    }
}
