using UnityEngine;

namespace Infrasctructure
{
    public class LoadingCurtain : MonoBehaviour
    {
        private void Awake()
        {
            DontDestroyOnLoad(this);
        }

        public void EnableLoadingCurtain()
        {
            gameObject.SetActive(true);
        }

        public void DisableLoadingCurtain()
        {
            gameObject.SetActive(false);
        }
    }
}