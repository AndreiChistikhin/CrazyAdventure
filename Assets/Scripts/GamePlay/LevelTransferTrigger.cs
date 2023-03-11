using GamePlay.Hero;
using Services;
using UnityEngine;
using Zenject;

public class LevelTransferTrigger : MonoBehaviour
{
    [SerializeField] private string _transfferTo;
    
    private IGameStateMachine _gameStateMachine;
    private IProgressService _progressService;
    private bool _triggered;

    [Inject]
    private void Construct(IGameStateMachine gameStateMachine, IProgressService progressService)
    {
        _progressService = progressService;
        _gameStateMachine = gameStateMachine;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_triggered)
            return;

        if (!other.gameObject.TryGetComponent(out HeroHealth hero)) 
            return;
        
        _progressService.GameProgress.WorldProgress.SceneToLoadName = _transfferTo;
        _gameStateMachine.Enter<LoadLevelState>();
        _triggered = true;
    }
}
