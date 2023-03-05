using System;
using GamePlay.HUD;
using UnityEngine;

namespace GamePlay.Hero
{
    public class HeroHealth : MonoBehaviour, IHealth, IProgressHandler
    {
        public float Maximum { get; set; }
        public float Current { get; set; }

        public event Action HealthChanged;
        
        public void TakeDamage(float damage)
        {
            Current -= damage;
            HealthChanged?.Invoke();
        }

        public void LoadProgress(GameProgress gameProgress)
        {
            Maximum = gameProgress.PlayerProgress.MaxHp;
            Current = gameProgress.PlayerProgress.CurrentHp;
        }

        public void SaveProgress(GameProgress gameProgress)
        {
            gameProgress.PlayerProgress.MaxHp = Maximum;
            gameProgress.PlayerProgress.CurrentHp = Current;
        }
    }
}