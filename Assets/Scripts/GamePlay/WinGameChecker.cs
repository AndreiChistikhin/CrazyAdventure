using Configs;
using Services;
using Services.Interfaces;
using UnityEngine;
using Zenject;

namespace GamePlay
{
    public class WinGameChecker : MonoBehaviour
    {
        private const string LastLevelName = "SecondLevelScene";
        private IConfigService _configService;
        private IProgressService _progressService;
        private IWindowService _windowService;

        [Inject]
        private void Construct(IConfigService configService, IProgressService progressService, IWindowService windowService)
        {
            _windowService = windowService;
            _progressService = progressService;
            _configService = configService;
        }

        private void OnEnable()
        {
            _progressService.GameProgress.EnemyProgress.EnemyWasKilled += CheckIfAllEnemiesDefeated;
        }

        private void OnDisable()
        {
            _progressService.GameProgress.EnemyProgress.EnemyWasKilled -= CheckIfAllEnemiesDefeated;
        }
    
        private async void CheckIfAllEnemiesDefeated()
        {
            EnemyPositions enemyPositions = await _configService.ForEnemyPositions(LastLevelName);
            foreach (EnemySpawner enemySpawner in enemyPositions.EnemySpawner)
            {
                if (!_progressService.GameProgress.EnemyProgress.ClearedSpawners.Contains(enemySpawner.EnemyId))
                    return;
            }
        
            _windowService.Open(WindowId.WinGame);
        }
    }
}
