using Configs;
using Cysharp.Threading.Tasks;
using GamePlay.Configs;

namespace Services
{
    public interface IConfigService : IService
    {
        UniTask<PlayerConfig> ForPlayer();
        UniTask<WorldConfig> ForWorld();
        UniTask<LevelConfig> ForSpawners(string level);
        UniTask<WindowParameters> ForWindow(WindowId loseGame);
    }
}