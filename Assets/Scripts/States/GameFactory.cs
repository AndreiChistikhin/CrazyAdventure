using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Services;
using UnityEngine;

public class GameFactory : IGameFactory
{
    private readonly IAssetProvider _assetProvider;
    public List<IProgressHandler> ProgressHandlers { get; } = new List<IProgressHandler>();

    public GameFactory(IAssetProvider assetProvider)
    {
        _assetProvider = assetProvider;
    }

    public void Warmup()
    {
    }

    public void CleanUp()
    {
    }

    public async UniTask<GameObject> CreatePlayer()
    {
        return await InstantiateRegistered(AssetsAddress.Player, new Vector3(9, 1, -8));
    }

    private async UniTask<GameObject> InstantiateRegistered(string path, Vector3 position)
    {
        GameObject gameObject = await _assetProvider.Instantiate(path, position);
        foreach (IProgressHandler progressHandler in gameObject.GetComponentsInChildren<IProgressHandler>())
        {
            ProgressHandlers.Add(progressHandler);
        }

        return gameObject;
    }
}