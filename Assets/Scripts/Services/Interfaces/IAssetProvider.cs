using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Services.Interfaces
{
    public interface IAssetProvider : IService
    {
        UniTask<GameObject> Instantiate(string address);
        UniTask<GameObject> Instantiate(string address, Vector3 initialPoint);
        UniTask<GameObject> Instantiate(string address, Transform under);
        UniTask<T> Load<T>(AssetReferenceGameObject assetReference) where T : class;
        void CleanUp();
        Task<T> Load<T>(string address) where T : class;
    }
}