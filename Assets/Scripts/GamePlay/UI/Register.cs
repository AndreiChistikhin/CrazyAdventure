using System;
using Infrasctructure;
using Services;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GamePlay.UI
{
    public class Register : MonoBehaviour
    {
        [SerializeField] private TMP_InputField _userName;
        [SerializeField] private TMP_InputField _password;
        [SerializeField] private Button _sendButton;
        [SerializeField] private Button _changeToRegisterButton;
        [SerializeField] private TMP_Text _errorText;
        
        private Action _openLogIn;
        private IServerRequester _serverRequester;

        public void Construct(IServerRequester serverRequester, Action openLogIn)
        {
            _serverRequester = serverRequester;
            _openLogIn = openLogIn;
            _sendButton.onClick.AddListener(SendRegister);
            _changeToRegisterButton.onClick.AddListener(_openLogIn.Invoke);
        }

        private async void SendRegister()
        {
            Credentials credentials = new Credentials {UserName = _userName.text, Password = _password.text};

            string token;
            try
            {
                token = await _serverRequester.Post<string>(ServerAPI.RegisterAPi, credentials);
                _serverRequester.Token = token;
            }
            catch (Exception e)
            {
                _errorText.gameObject.SetActive(true);
                throw;
            }
        }
        
        private void OnDestroy()
        {
            _sendButton.onClick.RemoveListener(SendRegister);
            _changeToRegisterButton.onClick.RemoveListener(_openLogIn.Invoke);
        }

    }
}