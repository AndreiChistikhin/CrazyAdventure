using Services;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WinGameWindow : MonoBehaviour
{
    [SerializeField] private Button _startAgainButton;
    [SerializeField] private TMP_Text _pointsText;

    private IProgressService _progressService;

    public void Construct(IProgressService progressService)
    {
        _progressService = progressService;
        _progressService.GameProgress.LootProgress.Changed += ChangePointsAmount;
        _startAgainButton.onClick.AddListener(QuitGame);
    }

    private void OnDestroy()
    {
        _progressService.GameProgress.LootProgress.Changed -= ChangePointsAmount;
        _startAgainButton.onClick.RemoveListener(QuitGame);
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