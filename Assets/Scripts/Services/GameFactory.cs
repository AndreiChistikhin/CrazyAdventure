using System.Collections.Generic;
using Configs;
using Cysharp.Threading.Tasks;
using GamePlay.Hero;
using Services;
using UnityEngine;
using Zenject;

public class GameFactory : IGameFactory
{
    private readonly IAssetProvider _assetProvider;
    private readonly DiContainer _diContainer;
    public List<IProgressHandler> ProgressHandlers { get; } = new List<IProgressHandler>();

    public GameFactory(IAssetProvider assetProvider, DiContainer diContainer)
    {
        _assetProvider = assetProvider;
        _diContainer = diContainer;
    }

    public void Warmup()
    {
    }

    public void CleanUp()
    {
    }

    public async UniTask<GameObject> CreatePlayer()
    {
        GameObject player = await InstantiateRegistered(AssetsAddress.Player);
        _diContainer.Inject(player.GetComponent<PlayerMovement>());
        return player;
    }

    public async UniTask<GameObject> CreateHUD()
    {
        return await InstantiateRegistered(AssetsAddress.HUD);
    }

    public async UniTask CreateSpawner(EnemySpawner spawner)
    {
        await InstantiateRegistered(AssetsAddress.Enemy, spawner.SpawnPosition);
    }

    private async UniTask<GameObject> InstantiateRegistered(string path, Vector3 position = default)
    {
        GameObject gameObject = await _assetProvider.Instantiate(path, position);
        foreach (IProgressHandler progressHandler in gameObject.GetComponentsInChildren<IProgressHandler>())
        {
            ProgressHandlers.Add(progressHandler);
        }

        return gameObject;
    }
}