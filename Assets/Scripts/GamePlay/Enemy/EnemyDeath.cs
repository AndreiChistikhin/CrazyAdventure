using System;
using System.Collections;
using UnityEngine;

namespace GamePlay.Enemy
{
    public class EnemyDeath : MonoBehaviour
    {
        [SerializeField] private EnemyHealth _health;
        [SerializeField] private EnemyAnimation _animator;
        [SerializeField] private Follow _enemyMove;
        [SerializeField] private SphereCollider _aggroObserver;
    
        public event Action OnDeath;
    
        private void OnEnable()
        {
            _health.HealthChanged += HealthChanged;
        }

        private void OnDisable()
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
            _animator.PlayDeath();

            _enemyMove.enabled = false;

            _aggroObserver.enabled = false;

            StartCoroutine(DestroyTimer());
            
            OnDeath?.Invoke();
        }

        private IEnumerator DestroyTimer()
        {
            yield return new WaitForSeconds(3);
            Destroy(gameObject);
        }
    }
}

