using System;

namespace GamePlay.HUD
{
    public interface IHealth
    {
        float Maximum { get; set; }
        float Current { get; set; }

        event Action HealthChanged;

        void TakeDamage(float damage);
    }
}