using UnityEngine;
using UnityEngine.UI;

public class PageChanger : MonoBehaviour
{
    [SerializeField] private Button _pageChangeButton;
    [SerializeField] private GameObject _pageToShow;

    private void Start()
    {
        _pageChangeButton.onClick.AddListener(ChangePage);
    }

    private void OnDestroy()
    {
        _pageChangeButton.onClick.RemoveListener(ChangePage);
    }

    private void ChangePage()
    {
        gameObject.SetActive(false);
        _pageToShow.SetActive(true);
    }
}
