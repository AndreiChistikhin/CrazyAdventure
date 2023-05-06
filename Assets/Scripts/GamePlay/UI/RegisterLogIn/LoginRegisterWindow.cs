using Infrasctructure;
using Services;
using UnityEngine;
using Zenject;

namespace GamePlay.UI
{
    public class LoginRegisterWindow : MonoBehaviour
    {
        [SerializeField] private CredentialsSender _logInWindow;
        [SerializeField] private CredentialsSender _registerInWindow;
        
        [Inject]
        private void Construct(IServerRequester serverRequester)
        {
            _logInWindow.Construct(serverRequester, ServerAPI.LogInAPi);
            _registerInWindow.Construct(serverRequester, ServerAPI.RegisterAPi);
        }
    }
}