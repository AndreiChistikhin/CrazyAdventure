using UnityEngine;
using Zenject;

public class Bootstrapper : MonoBehaviour
{
    private IGameStateMachine _stateMachine;

    [Inject]
    public void Construct(IGameStateMachine stateMachine)
    {
        _stateMachine = stateMachine;
    }
    
    private void Awake()
    {
        _stateMachine.Enter<BootstrapState>();
    }
}