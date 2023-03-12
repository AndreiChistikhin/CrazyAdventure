using Configs;
using Cysharp.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IConfigService : IService
    {
        UniTask<PlayerConfig> ForPlayer();
        UniTask<WorldConfig> ForWorld();
        UniTask<EnemyPositions> ForLevel(string level);
        UniTask<WindowParameters> ForWindow(WindowId loseGame);
        UniTask<SpawnersConfig> ForSpawners();
    }
}