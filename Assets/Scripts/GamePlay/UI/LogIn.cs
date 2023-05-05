using System;
using Infrasctructure;
using Services;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GamePlay.UI
{
    public class LogIn : MonoBehaviour
    {
        [SerializeField] private TMP_InputField _userName;
        [SerializeField] private TMP_InputField _password;
        [SerializeField] private Button _sendButton;
        [SerializeField] private Button _changeToRegisterButton;
        [SerializeField] private TMP_Text _errorText;
        
        private IServerRequester _serverRequester;
        private Action _openRegister;

        public void Construct(IServerRequester serverRequester, Action openRegister)
        {
            _openRegister = openRegister;
            _serverRequester = serverRequester;
            _sendButton.onClick.AddListener(SendLogIn);
            _changeToRegisterButton.onClick.AddListener(_openRegister.Invoke);
        }

        private async void SendLogIn()
        {
            Credentials credentials = new Credentials {UserName = _userName.text, Password = _password.text};

            string token;
            try
            {
                token = await _serverRequester.Post<string>(ServerAPI.LogInAPi, credentials);
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
            _sendButton.onClick.RemoveListener(SendLogIn);
            _changeToRegisterButton.onClick.RemoveListener(_openRegister.Invoke);
        }
    }
}

public class Credentials
{
    public string UserName { get; set; }
    public string Password { get; set; }
}
