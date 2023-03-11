using System.Collections.Generic;
using Configs;
using Cysharp.Threading.Tasks;
using GamePlay.Enemy;
using GamePlay.Hero;
using GamePlay.HUD;
using Services;
using UnityEngine;
using Zenject;

public class GameFactory : IGameFactory
{
    private readonly IAssetProvider _assetProvider;
    private readonly DiContainer _diContainer;
    private readonly IProgressService _progressService;
    private readonly IConfigService _configService;
    private readonly IWindowService _windowService;
    public List<IProgressHandler> ProgressHandlers { get; } = new List<IProgressHandler>();

    private List<GameObject> _listOfInstantiatedObjects = new List<GameObject>();
    private GameObject _player;

    public GameFactory(IAssetProvider assetProvider, DiContainer diContainer, IProgressService progressService,
        IConfigService configService, IWindowService windowService)
    {
        _assetProvider = assetProvider;
        _diContainer = diContainer;
        _progressService = progressService;
        _configService = configService;
        _windowService = windowService;
    }

    public void Warmup()
    {
    }

    public void CleanUp()
    {
        ProgressHandlers.Clear();
        DestroyAllInstantiatedObjects();
        _assetProvider.CleanUp();
    }

    public async UniTask<GameObject> CreatePlayer()
    {
        _player = await InstantiateRegistered(AssetsAddress.Player);
        _diContainer.Inject(_player.GetComponent<PlayerMovement>());
        _diContainer.Inject(_player.GetComponent<HeroAttack>());
        _diContainer.Inject(_player.GetComponent<HeroDeath>());
        return _player;
    }

    public async UniTask<GameObject> CreateHUD()
    {
        GameObject hud = await InstantiateRegistered(AssetsAddress.HUD);

        hud.GetComponentInChildren<LootCount>().Construct(_progressService.GameProgress);

        return hud;
    }

    public async UniTask CreateEnemy(EnemySpawner spawner, string enemyId)
    {
        GameObject enemy = await InstantiateRegistered(AssetsAddress.Enemy, spawner.SpawnPosition);
        enemy.GetComponent<EnemyMoveToPlayer>().Construct(_player.transform);
        enemy.GetComponent<ActorUI>().Construct(enemy.GetComponent<IHealth>());
        enemy.GetComponent<Attack>().Construct(_player.transform);
        enemy.GetComponentInChildren<LootSpawner>().Construct(this);
        enemy.GetComponent<EnemyDeath>().Construct(_configService, _progressService, _windowService);
        enemy.GetComponent<EnemyDeath>().OnDeath +=
            () => _progressService.GameProgress.EnemyProgress.ClearedSpawners.Add(enemyId);
    }

    public async UniTask<LootPiece> CreateLoot()
    {
        GameObject prefab = await InstantiateRegistered(AssetsAddress.Loot);
        LootPiece lootPiece = prefab.GetComponent<LootPiece>();

        lootPiece.Construct(_progressService.GameProgress);

        return lootPiece;
    }

    private async UniTask<GameObject> InstantiateRegistered(string path, Vector3 position = default)
    {
        GameObject gameObject = await _assetProvider.Instantiate(path);
        gameObject.transform.position = position;
        foreach (IProgressHandler progressHandler in gameObject.GetComponentsInChildren<IProgressHandler>())
        {
            ProgressHandlers.Add(progressHandler);
        }

        _listOfInstantiatedObjects.Add(gameObject);

        return gameObject;
    }

    private void DestroyAllInstantiatedObjects()
    {
        foreach (GameObject instantiatedObject in _listOfInstantiatedObjects)
        {
            Object.Destroy(instantiatedObject);
        }

        _listOfInstantiatedObjects.Clear();
    }
}