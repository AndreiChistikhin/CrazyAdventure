using Services;
using UnityEngine;
using Zenject;

public class GameSaver : MonoBehaviour
{
    [SerializeField] private BoxCollider _collider;

    private ISaveLoadService _saveLoadService;

    [Inject]
    private void Construct(ISaveLoadService saveLoadService)
    {
        _saveLoadService = saveLoadService;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        _saveLoadService.SaveProgress();
        Debug.Log("Progress Saved");
        gameObject.SetActive(false);
    }

    private void OnDrawGizmos()
    {
        if (_collider == null)
            return; 
            
        Gizmos.color = new Color32(30, 200, 30, 130);
        Gizmos.DrawCube(transform.position + _collider.center, _collider.size);
    }
}
