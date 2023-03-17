using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Services.Interfaces
{
    public interface IAssetProvider : IService
    {
        Task<T> Load<T>(string address) where T : class;
        UniTask<GameObject> Instantiate(string address);
        void CleanUp();
    }
}