using System;
using System.Collections;
using Configs;
using Cysharp.Threading.Tasks;
using GamePlay.Enemy;
using Services;
using UnityEngine;

public class EnemyDeath : MonoBehaviour
{
    [SerializeField] private EnemyHealth _health;
    [SerializeField] private EnemyAnimation _animator;
    [SerializeField] private Follow _enemyMove;
    [SerializeField] private SphereCollider _aggroObserver;

    private const string LastLevelName = "SecondLevelScene";
    private IConfigService _configService;
    private IProgressService _progressService;
    private IWindowService _windowService;

    public event Action OnDeath;

    public void Construct(IConfigService configService, IProgressService progressService, IWindowService windowService)
    {
        _windowService = windowService;
        _progressService = progressService;
        _configService = configService;
    }

    private void Start()
    {
        _health.HealthChanged += HealthChanged;
    }

    private void OnDestroy()
    {
        _health.HealthChanged -= HealthChanged;
    }

    private void HealthChanged()
    {
        if (_health.Current <= 0)
            Die();
    }

    private async void Die()
    {
        _health.HealthChanged -= HealthChanged;

        _animator.PlayDeath();

        _enemyMove.enabled = false;

        _aggroObserver.enabled = false;

        StartCoroutine(DestroyTimer());
            
        OnDeath?.Invoke();

        bool gameIsOver = await AllEnemiesDefeated();
        
        if(gameIsOver)
        {
            _windowService.Open(WindowId.WinGame);
        }
    }

    private IEnumerator DestroyTimer()
    {
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }

    private async UniTask<bool> AllEnemiesDefeated()
    {
        LevelConfig levelConfig = await _configService.ForSpawners(LastLevelName);
        foreach (EnemySpawner enemySpawner in levelConfig.EnemySpawner)
        {
            if (!_progressService.GameProgress.EnemyProgress.ClearedSpawners.Contains(enemySpawner.EnemyId))
                return false;
        }

        return true;
    }
}

