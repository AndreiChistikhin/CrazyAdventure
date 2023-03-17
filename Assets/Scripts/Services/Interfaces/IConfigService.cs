using Configs;
using Cysharp.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IConfigService : IService
    {
        UniTask<PlayerConfig> ForPlayer();
        UniTask<LevelStartConfig> ForWorld();
        UniTask<EnemyPositions> ForEnemyPositions(string level);
        UniTask<WindowParameters> ForWindow(WindowId loseGame);
        UniTask<EnemyConfig> ForSpawners();
        UniTask<LevelStartingPoint> ForLevel(string levelName);
    }
}