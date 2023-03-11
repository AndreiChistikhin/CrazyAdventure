using Services;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class LoseGameWindow : MonoBehaviour
{
    [SerializeField] private Button _startAgainButton;
    [SerializeField] private TMP_Text _pointsText;

    private const string FirstLevelName = "FirstLevelScene";
    private IGameStateMachine _gameStateMachine;
    private IProgressService _progressService;

    [Inject]
    public void Construct(IProgressService progressService, IGameStateMachine gameStateMachine)
    {
        _gameStateMachine = gameStateMachine;
        _progressService = progressService;
        _progressService.GameProgress.LootProgress.Changed += ChangePointsAmount;
        _startAgainButton.onClick.AddListener(StartGameAgain);
        ChangePointsAmount();
    }

    private void OnDestroy()
    {
        _progressService.GameProgress.LootProgress.Changed -= ChangePointsAmount;
        _startAgainButton.onClick.RemoveListener(StartGameAgain);
    }

    private void ChangePointsAmount()
    {
        _pointsText.text = _progressService.GameProgress.LootProgress.LootCount.ToString();
    }

    private void StartGameAgain()
    {
        _progressService.GameProgress.WorldProgress.SceneToLoadName = FirstLevelName; 
        _gameStateMachine.Enter<LoadLevelState>();
    }
}