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

        private List<GameObject> _instantiatedObjects = new List<GameObject>();

        public UIFactory(IAssetProvider assets, IConfigService staticData,
            IProgressService progressService, DiContainer diContainer)
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
            _instantiatedObjects.Add(instantiate);
        }

        public async UniTask CreateLoseGameMenu()
        {
            GameObject window = await CreateWindow(WindowId.LoseGame);
            _diContainer.Inject(window.GetComponent<LoseGameWindow>());
        }

        public async UniTask CreateWinGameMenu()
        {
            GameObject window = await CreateWindow(WindowId.WinGame);
            window.GetComponent<WinGameWindow>().Construct(_progressService);
        }

        private async UniTask<GameObject> CreateWindow(WindowId windowId)
        {
            WindowParameters config = await _staticData.ForWindow(windowId);
            GameObject window = Object.Instantiate(config.WindowPrefab, _uiRoot);
            _instantiatedObjects.Add(window);
            return window;
        }

        public void CleanUp()
        {
            foreach (GameObject gameObject in _instantiatedObjects)
            {
                Object.Destroy(gameObject);
            }
        }
    }
}