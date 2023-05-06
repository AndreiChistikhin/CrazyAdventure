using System;
using Services;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GamePlay.UI
{
    public class CredentialsSender : MonoBehaviour
    {
        [SerializeField] private TMP_InputField _userName;
        [SerializeField] private TMP_InputField _password;
        [SerializeField] private Button _sendButton;
        [SerializeField] private TMP_Text _errorText;
        
        private IServerRequester _serverRequester;
        private string _serverAPI;

        public void Construct(IServerRequester serverRequester, string serverAPI)
        {
            _serverAPI = serverAPI;
            _serverRequester = serverRequester;
            _sendButton.onClick.AddListener(SendCredentials);
        }

        private void OnDestroy()
        {
            _sendButton.onClick.RemoveListener(SendCredentials);
        }

        private async void SendCredentials()
        {
            Credentials credentials = new Credentials {UserName = _userName.text, Password = _password.text};

            string token;
            try
            {
                token = await _serverRequester.Post<string>(_serverAPI, credentials);
                _serverRequester.Token = token;
            }
            catch (Exception e)
            {
                _errorText.gameObject.SetActive(true);
                throw;
            }
        }
    }
}