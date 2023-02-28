using System;
using Cysharp.Threading.Tasks;

public interface ISceneLoader : IService
{
    UniTaskVoid LoadScene(string sceneName, Action onLoaded = null);
}