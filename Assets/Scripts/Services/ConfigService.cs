using System.Linq;
using Configs;
using Cysharp.Threading.Tasks;
using Infrasctructure;
using Services.Interfaces;

namespace Services
{
    public class ConfigService : IConfigService
    {
        private IAssetProvider _assetProvider;

        public ConfigService(IAssetProvider assetProvider)
        {
            _assetProvider = assetProvider;
        }
        
        public async UniTask<PlayerConfig> ForPlayer()
        {
            return await _assetProvider.Load<PlayerConfig>(AssetsAddress.PlayerConfig);
        }

        public async UniTask<LevelStartConfig> ForWorld()
        {
            return await _assetProvider.Load<LevelStartConfig>(AssetsAddress.WorldConfig);
        }
        
        public async UniTask<LevelStartingPoint> ForLevel(string levelName)
        {
            LevelStartConfig levelStartConfig = await _assetProvider.Load<LevelStartConfig>(AssetsAddress.WorldConfig);
            return levelStartConfig.StartingPoints.FirstOrDefault(x => x.LevelName == levelName);
        }

        public async UniTask<EnemyPositions> ForEnemyPositions(string level)
        {
            EnemyConfig enemyConfig = await _assetProvider.Load<EnemyConfig>(AssetsAddress.EnemyConfig);
            return enemyConfig.EnemyPositions.FirstOrDefault(x => x.SceneName == level);
        }

        public async UniTask<WindowParameters> ForWindow(WindowId windowId)
        {
            WindowConfig spawnersConfig = await _assetProvider.Load<WindowConfig>(AssetsAddress.WindowConfig);
            return spawnersConfig.Windows.FirstOrDefault(x => x.WindowId == windowId);
        }

        public async UniTask<EnemyConfig> ForSpawners()
        {
            return await _assetProvider.Load<EnemyConfig>(AssetsAddress.EnemyConfig);
        }
    }
}