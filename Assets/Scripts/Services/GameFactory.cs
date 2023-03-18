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

        private GameObject _player;

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
            _assetProvider.CleanUp();
        }

        public async UniTask<GameObject> CreatePlayer()
        {
            _player = await InstantiateRegisteredAsync(AssetsAddress.Player);
            _diContainer.Inject(_player.GetComponent<PlayerMovement>());
            _diContainer.Inject(_player.GetComponent<HeroAttack>());
            _diContainer.Inject(_player.GetComponent<HeroDeath>());
            return _player;
        }

        public async UniTask<GameObject> CreateHUD()
        {
            GameObject hud = await InstantiateRegisteredAsync(AssetsAddress.HUD);

            hud.GetComponentInChildren<LootCount>().Construct(_progressService.GameProgress);

            return hud;
        }

        public async UniTask CreateEnemy(EnemySpawner spawner, string enemyId)
        {
            EnemyConfig enemyConfig = await _configService.ForSpawners();
            
            GameObject prefab = await _assetProvider.Load<GameObject>(AssetsAddress.Enemy);
            GameObject enemy = Object.Instantiate(prefab, spawner.SpawnPosition, Quaternion.identity);
            
            enemy.GetComponent<EnemyMoveToPlayer>()?.Construct(_player.transform);
            
            IHealth health = enemy.GetComponent<IHealth>();
            health.Maximum = enemyConfig.EnemyInitialHealth;
            health.Current = enemyConfig.EnemyInitialHealth;
            enemy.GetComponent<ActorUI>()?.Construct(enemy.GetComponent<IHealth>());
            
            enemy.GetComponent<Attack>()?.Construct(_player.transform, enemyConfig.EnemyDamage);
            

            LootSpawner lootSpawner = enemy.GetComponentInChildren<LootSpawner>();
            lootSpawner.Construct(this);
            lootSpawner.SetLoot(enemyConfig.MinimumLoot, enemyConfig.MaximumLoot);
            enemy.GetComponent<EnemyDeath>().OnDeath +=
                () => _progressService.GameProgress.EnemyProgress.AddKilledEnemy(enemyId);
        }

        public async UniTask<LootPiece> CreateLoot()
        {
            GameObject prefab = await _assetProvider.Load<GameObject>(AssetsAddress.Loot);
            LootPiece lootPiece = InstantiateRegistered(prefab).GetComponent<LootPiece>();

            lootPiece.Construct(_progressService.GameProgress);

            return lootPiece;
        }

        private async UniTask<GameObject> InstantiateRegisteredAsync(string path, Vector3 position = default)
        {
            GameObject gameObject = await _assetProvider.Instantiate(path);
            gameObject.transform.position = position;
            RegisterProgressWatchers(gameObject);

            return gameObject;
        }

        private GameObject InstantiateRegistered(GameObject prefab, Vector3 position = default)
        {
            GameObject gameObject = Object.Instantiate(prefab);
            gameObject.transform.position = position;

            RegisterProgressWatchers(gameObject);

            return gameObject;
        }

        private void RegisterProgressWatchers(GameObject gameObject)
        {
            foreach (IProgressHandler progressHandler in gameObject.GetComponentsInChildren<IProgressHandler>())
            {
                ProgressHandlers.Add(progressHandler);
            }
        }
    }
}