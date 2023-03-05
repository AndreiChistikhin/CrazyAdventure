using UnityEngine;

namespace GamePlay.HUD
{
    public class ActorUI : MonoBehaviour
    {
        [SerializeField] private HpBar _hpBar;
        
        private IHealth _health;

        public void Construct(IHealth health)
        {
            _health = health;
            _health.HealthChanged += UpdateHpBar;
        }
        
        private void OnDestroy()
        {
            _health.HealthChanged -= UpdateHpBar;
        }
        
        private void UpdateHpBar()
        {
            _hpBar.SetNewHealth(_health.Current, _health.Maximum);
        }
    }
}