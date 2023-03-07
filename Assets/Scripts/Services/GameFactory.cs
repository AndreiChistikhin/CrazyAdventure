using System.Collections.Generic;
using Configs;
using Cysharp.Threading.Tasks;
using GamePlay.Enemy;
using GamePlay.Hero;
using Services;
using UnityEngine;
using Zenject;

public class GameFactory : IGameFactory
{
    private readonly IAssetProvider _assetProvider;
    private readonly DiContainer _diContainer;
    public List<IProgressHandler> ProgressHandlers { get; } = new List<IProgressHandler>();

    private GameObject _player;

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
        _player = await InstantiateRegistered(AssetsAddress.Player);
        _diContainer.Inject(_player.GetComponent<PlayerMovement>());
        _diContainer.Inject(_player.GetComponent<HeroAttack>());
        return _player;
    }

    public async UniTask<GameObject> CreateHUD()
    {
        return await InstantiateRegistered(AssetsAddress.HUD);
    }

    public async UniTask CreateEnemy(EnemySpawner spawner)
    {
        GameObject enemy = await InstantiateRegistered(AssetsAddress.Enemy, spawner.SpawnPosition);
        enemy.GetComponent<EnemyMoveToPlayer>().Construct(_player.transform);
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