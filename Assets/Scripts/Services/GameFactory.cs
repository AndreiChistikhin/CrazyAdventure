using System.Collections.Generic;
using Configs;
using Cysharp.Threading.Tasks;
using GamePlay.Enemy;
using GamePlay.Hero;
using GamePlay.HUD;
using GamePlay.Loot;
using Infrasctructure;
using Progress;
using Services.Interfaces;
using UnityEngine;
using Zenject;

namespace Services
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssetProvider _assetProvider;
        private readonly IProgressService _progressService;
        private readonly IConfigService _configService;
        private readonly DiContainer _diContainer;
        public List<IProgressHandler> ProgressHandlers { get; } = new List<IProgressHandler>();

        private List<GameObject> _listOfInstantiatedObjects = new List<GameObject>();
        private GameObject _player;
        private EnemyConfig _enemyConfig;

        public GameFactory(IAssetProvider assetProvider, IProgressService progressService, IConfigService configService,
            DiContainer diContainer)
        {
            _assetProvider = assetProvider;
            _progressService = progressService;
            _configService = configService;
            _diContainer = diContainer;
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
            _enemyConfig = await _configService.ForSpawners();
            GameObject enemy = await InstantiateRegistered(AssetsAddress.Enemy, spawner.SpawnPosition);
            enemy.GetComponent<EnemyMoveToPlayer>().Construct(_player.transform);
            enemy.GetComponent<ActorUI>().Construct(enemy.GetComponent<IHealth>());
            enemy.GetComponent<Attack>().Construct(_player.transform);

            LootSpawner lootSpawner = enemy.GetComponentInChildren<LootSpawner>();
            lootSpawner.Construct(this);
            lootSpawner.SetLoot(_enemyConfig.MinimumLoot, _enemyConfig.MaximumLoot);
            enemy.GetComponent<EnemyDeath>().OnDeath +=
                () => _progressService.GameProgress.EnemyProgress.AddKilledEnemy(enemyId);
        }

        public async UniTask<LootPiece> CreateLoot()
        {
            GameObject prefab = await InstantiateRegistered(AssetsAddress.Loot);

            prefab.GetComponent<LootPiece>().Construct(_progressService.GameProgress);

            return prefab.GetComponent<LootPiece>();
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
}