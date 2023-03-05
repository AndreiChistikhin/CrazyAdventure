using System.Linq;
using Configs;
using Cysharp.Threading.Tasks;
using GamePlay.Configs;

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

        public async UniTask<LevelConfig> ForSpawners(string level)
        {
            SpawnersConfig spawnersConfig = await _assetProvider.Load<SpawnersConfig>(AssetsAddress.SpawnerConfig);
            return spawnersConfig.LevelConfigs.FirstOrDefault(x => x.SceneName == level);
        }
    }
}