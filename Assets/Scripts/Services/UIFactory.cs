using System.Collections.Generic;
using System.Threading.Tasks;
using Configs;
using Cysharp.Threading.Tasks;
using GamePlay.UI;
using Infrasctructure;
using Services.Interfaces;
using UnityEngine;
using Zenject;

namespace Services
{
    public class UIFactory : IUIFactory
    {
        private readonly IAssetProvider _assets;
        private readonly IConfigService _staticData;
        private readonly IProgressService _progressService;
        private readonly DiContainer _diContainer;

        private Transform _uiRoot;

        public UIFactory(IAssetProvider assets, IConfigService staticData,
            IProgressService progressService, IServerRequester serverRequester, DiContainer diContainer)
        {
            _assets = assets;
            _staticData = staticData;
            _progressService = progressService;
            _diContainer = diContainer;
        }

        public async UniTask CreateUIRoot()
        {
            GameObject instantiate = await _assets.Instantiate(AssetsAddress.UIRootPath);
            _uiRoot = instantiate.transform;
        }

        public async UniTask CreateLoseGameMenu()
        {
            GameObject window = await CreateWindow(WindowId.LoseGame);
            _diContainer.Inject(window.GetComponent<LoseGameWindow>());
        }

        public async UniTask CreateWinGameMenu()
        {
            GameObject window = await CreateWindow(WindowId.WinGame);
            _diContainer.Inject(window.GetComponent<WinGameWindow>());
        }

        public async UniTask CreateLoginRegisterWindow()
        {
            GameObject window = await CreateWindow(WindowId.LoginRegister);
            _diContainer.Inject(window.GetComponent<LoginRegisterWindow>());
        }

        private async UniTask<GameObject> CreateWindow(WindowId windowId)
        {
            WindowParameters config = await _staticData.ForWindow(windowId);
            GameObject window = Object.Instantiate(config.WindowPrefab, _uiRoot);
            return window;
        }
    }
}