using Services;
using Services.Interfaces;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GamePlay.UI
{
    public class WinGameWindow : MonoBehaviour
    {
        [SerializeField] private Button _quitGameButton;
        [SerializeField] private TMP_Text _pointsText;

        private IProgressService _progressService;

        public void Construct(IProgressService progressService)
        {
            _progressService = progressService;
            _progressService.GameProgress.LootProgress.Changed += ChangePointsAmount;
            _quitGameButton.onClick.AddListener(QuitGame);
            ChangePointsAmount();
        }

        private void OnDestroy()
        {
            _progressService.GameProgress.LootProgress.Changed -= ChangePointsAmount;
            _quitGameButton.onClick.RemoveListener(QuitGame);
        }

        private void ChangePointsAmount()
        {
            _pointsText.text = _progressService.GameProgress.LootProgress.LootCount.ToString();
        }

        private void QuitGame()
        {
            PlayerPrefs.DeleteAll();
            Application.Quit();
        }
    }
}