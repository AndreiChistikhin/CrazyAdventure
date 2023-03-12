using UnityEngine;
using UnityEngine.UI;

namespace GamePlay.HUD
{
    public class HpBar : MonoBehaviour
    {
        [SerializeField] private Image _hpBar;

        public void SetNewHealth(float currentHealth, float maximumHealth) =>
            _hpBar.fillAmount = currentHealth / maximumHealth;
    }
}