using System;
using Services;
using UnityEngine;
using Zenject;

namespace GamePlay.UI
{
    public class LoginRegisterWindow : MonoBehaviour
    {
        [SerializeField] private LogIn _logInWindow;
        [SerializeField] private Register _registerInWindow;
        
        private Action OpenLogIn => () => ChangePages(true);

        private Action OpenRegister => () => ChangePages(false);

        [Inject]
        private void Construct(IServerRequester serverRequester)
        {
            _logInWindow.Construct(serverRequester, OpenRegister);
            _registerInWindow.Construct(serverRequester, OpenLogIn);
        }

        private void ChangePages(bool activateLogIn)
        {
            _logInWindow.gameObject.SetActive(activateLogIn);
            _registerInWindow.gameObject.SetActive(!activateLogIn);
        }
    }
}