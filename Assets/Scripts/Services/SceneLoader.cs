using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class SceneLoader : ISceneLoader
{
    private LoadingCurtain _loadingCurtain;

    [Inject]
    public SceneLoader(LoadingCurtain loadingCurtain)
    {
        _loadingCurtain = loadingCurtain;
    }

    public async UniTaskVoid LoadScene(string sceneName, Action onLoaded = null)
    {
        if (SceneManager.GetActiveScene().name == sceneName)
        {
            onLoaded?.Invoke();
            return;
        }

        AsyncOperation loadSceneAsync = SceneManager.LoadSceneAsync(sceneName);
        _loadingCurtain.EnableLoadingCurtain();
        
        await loadSceneAsync.ToUniTask();
        
        _loadingCurtain.DisableLoadingCurtain();
        onLoaded?.Invoke();
    }
}