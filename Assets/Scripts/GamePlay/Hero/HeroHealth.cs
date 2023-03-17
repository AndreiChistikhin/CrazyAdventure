using System;
using GamePlay.HUD;
using Progress;
using UnityEngine;

namespace GamePlay.Hero
{
    public class HeroHealth : MonoBehaviour, IHealth, IProgressHandler
    {
        [SerializeField] private HeroAnimation _heroAnimation;
        
        public float Maximum { get; set; }
        public float Current { get; set; }

        public event Action HealthChanged;
        
        public void TakeDamage(float damage)
        {
            Current -= damage;
            _heroAnimation.PlayGetHit();
            HealthChanged?.Invoke();
        }

        public void LoadProgress(GameProgress gameProgress)
        {
            Maximum = gameProgress.PlayerProgress.MaxHp;
            Current = gameProgress.PlayerProgress.CurrentHp;
            HealthChanged?.Invoke();
        }

        public void SaveProgress(GameProgress gameProgress)
        {
            gameProgress.PlayerProgress.MaxHp = Maximum;
            gameProgress.PlayerProgress.CurrentHp = Current;
        }
    }
}