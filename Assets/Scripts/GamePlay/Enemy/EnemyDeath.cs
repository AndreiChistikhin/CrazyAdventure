using System;
using System.Collections;
using GamePlay.Enemy;
using UnityEngine;

public class EnemyDeath : MonoBehaviour
{
    [SerializeField] private EnemyHealth _health;
    [SerializeField] private EnemyAnimation _animator;

    public event Action OnDeath;

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

    private void Die()
    {
        _health.HealthChanged -= HealthChanged;

        _animator.PlayDeath();

        SpawnDeathFx();
        StartCoroutine(DestroyTimer());
            
        OnDeath?.Invoke();
    }

    private void SpawnDeathFx()
    {
        //Instantiate(_deathFX, transform.position, Quaternion.identity);
    }

    private IEnumerator DestroyTimer()
    {
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }
}
