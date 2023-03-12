using Configs;
using Services.Interfaces;
using UnityEngine;
using Zenject;

namespace GamePlay.Hero
{
    public class HeroDeath : MonoBehaviour
    {
        [SerializeField] private HeroHealth _heroHealth;
        [SerializeField] private PlayerMovement _movement;
        [SerializeField] private HeroAnimation _animator;
        [SerializeField] private HeroAttack _heroAttack;
        private bool _isDead;
        private IWindowService _windowService;

        [Inject]
        private void Construct(IWindowService windowService)
        {
            _windowService = windowService;
        }

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
            _movement.enabled = false;
            _heroAttack.enabled = false;
            _animator.PlayDied();
            _windowService.Open(WindowId.LoseGame);
        }
    }
}
