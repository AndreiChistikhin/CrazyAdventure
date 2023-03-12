using System;
using GamePlay.HUD;
using UnityEngine;

namespace GamePlay.Enemy
{
    public class EnemyHealth : MonoBehaviour, IHealth
    {
        [SerializeField] private EnemyAnimation _enemyAniamtor;
        [field: SerializeField] public float Maximum { get; set; }
        [field: SerializeField] public float Current { get; set; }

        public event Action HealthChanged;

        public void TakeDamage(float damage)
        {
            Current -= damage;

            if (Current > 0)
                _enemyAniamtor.PlayHit();

            HealthChanged?.Invoke();
        }
    }
}