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

        public async UniTask<WorldConfig> ForWorld()
        {
            return await _assetProvider.Load<WorldConfig>(AssetsAddress.WorldConfig);
        }

        public async UniTask<EnemyPositions> ForLevel(string level)
        {
            SpawnersConfig spawnersConfig = await _assetProvider.Load<SpawnersConfig>(AssetsAddress.EnemyConfig);
            return spawnersConfig.EnemyPositions.FirstOrDefault(x => x.SceneName == level);
        }

        public async UniTask<WindowParameters> ForWindow(WindowId windowId)
        {
            WindowConfig spawnersConfig = await _assetProvider.Load<WindowConfig>(AssetsAddress.WindowConfig);
            return spawnersConfig.Windows.FirstOrDefault(x => x.WindowId == windowId);
        }

        public async UniTask<SpawnersConfig> ForSpawners()
        {
            return await _assetProvider.Load<SpawnersConfig>(AssetsAddress.EnemyConfig);
        }
    }
}