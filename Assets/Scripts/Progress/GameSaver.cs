using System;
using Services;
using UnityEngine;
using Zenject;

public class GameSaver : MonoBehaviour
{
    private ISaveLoadService _saveLoadService;

    [Inject]
    private void Construct(ISaveLoadService saveLoadService)
    {
        _saveLoadService = saveLoadService;
    }

    private void OnTriggerEnter(Collider other)
    {
        SaveGame();
    }

    private void SaveGame()
    {
        _saveLoadService.SaveProgress();
        Debug.Log("Progress Saved");
        gameObject.SetActive(false);
    }
}
