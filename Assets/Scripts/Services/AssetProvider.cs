using System.Collections.Generic;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using GamePlay.Hero;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Zenject;

namespace Services
{
    public class AssetProvider : IAssetProvider
    {
        private readonly DiContainer _diContainer;

        private Dictionary<string, AsyncOperationHandle> _completedCache =
            new Dictionary<string, AsyncOperationHandle>();

        private Dictionary<string, List<AsyncOperationHandle>> _handles =
            new Dictionary<string, List<AsyncOperationHandle>>();

        public AssetProvider(DiContainer diContainer)
        {
            _diContainer = diContainer;
        }

        public void Initialize()
        {
            Addressables.InitializeAsync();
        }

        public async UniTask<T> Load<T>(AssetReferenceGameObject assetReference) where T : class
        {
            if (_completedCache.TryGetValue(assetReference.AssetGUID, out AsyncOperationHandle completedHandle))
                return completedHandle.Result as T;

            return await RunWithCacheOnComplete(Addressables.LoadAssetAsync<T>(assetReference),
                assetReference.AssetGUID);
        }

        public async Task<T> Load<T>(string address) where T : class
        {
            if (_completedCache.TryGetValue(address, out AsyncOperationHandle completedHandle))
                return completedHandle.Result as T;

            return await RunWithCacheOnComplete(Addressables.LoadAssetAsync<T>(address),
                address);
        }

        public async UniTask<GameObject> Instantiate(string address) => await Addressables.InstantiateAsync(address).Task;

        public async UniTask<GameObject> Instantiate(string address, Vector3 initialPoint)
        {
            return await Addressables.InstantiateAsync(address, initialPoint, Quaternion.identity).Task;
        }

        public async UniTask<GameObject> Instantiate(string address, Transform under) =>
            await Addressables.InstantiateAsync(address, under).Task;

        public void CleanUp()
        {
            foreach (List<AsyncOperationHandle> resourceHandles in _handles.Values)
            foreach (var handle in resourceHandles)
                Addressables.Release(handle);

            _completedCache.Clear();
            _handles.Clear();
        }

        private async Task<T> RunWithCacheOnComplete<T>(AsyncOperationHandle<T> handle, string cacheKey) where T : class
        {
            handle.Completed += completeHandle => { _completedCache[cacheKey] = completeHandle; };

            AddHandle(cacheKey, handle);

            return await handle.Task;
        }

        private void AddHandle<T>(string key, AsyncOperationHandle<T> handle) where T : class
        {
            if (!_handles.TryGetValue(key, out var resourceHandles))
            {
                resourceHandles = new List<AsyncOperationHandle>();
                _handles[key] = resourceHandles;
            }

            resourceHandles.Add(handle);
        }
    }
}