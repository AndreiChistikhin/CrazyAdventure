using GamePlay.Hero;
using UnityEngine;

public class HeroDeath : MonoBehaviour
{
    [SerializeField] private HeroHealth _heroHealth;
    [SerializeField] private PlayerMovement _cubeMove;
    [SerializeField] private HeroAnimation _animator;
    [SerializeField] private GameObject _deathFx;
    [SerializeField] private HeroAttack _heroAttack;
    private bool _isDead;

    private void Start() => _heroHealth.HealthChanged += HealthChanged;

    private void OnDestroy() => _heroHealth.HealthChanged -= HealthChanged;

    private void HealthChanged()
    {
        if (!_isDead && _heroHealth.Current <= 0)
            Die();
    }

    private void Die()
    {
        _isDead = true;
        _cubeMove.enabled = false;
        _heroAttack.enabled = false;
        _animator.PlayDied();

        Instantiate(_deathFx, transform.position, Quaternion.identity);
    }
}