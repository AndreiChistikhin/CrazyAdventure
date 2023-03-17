using UnityEngine;

namespace Infrasctructure
{
    public class LoadingCurtain : MonoBehaviour
    {
        [SerializeField] private GameObject _loadingCurtain;
        
        private void Awake()
        {
            DontDestroyOnLoad(this);
        }

        public void EnableLoadingCurtain()
        {
            _loadingCurtain.SetActive(true);
        }

        public void DisableLoadingCurtain()
        {
            _loadingCurtain.SetActive(false);
        }
    }
}