using Services.Interfaces;
using States;
using UnityEngine;
using Zenject;

namespace Infrasctructure
{
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
}