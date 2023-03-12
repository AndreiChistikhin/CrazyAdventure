using System;
using Cysharp.Threading.Tasks;

namespace Services.Interfaces
{
    public interface ISceneLoader : IService
    {
        UniTaskVoid LoadScene(string sceneName, Action onLoaded = null);
    }
}