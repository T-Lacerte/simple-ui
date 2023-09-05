using UnityEngine;
using UnityEngine.UI;

namespace SimpleUI.Core
{
    public class LoadingManager : MonoBehaviour
    {
        [SerializeField]
        private Image _loadingIcon;
        [SerializeField]
        private Image _loadingBackground;
        
        
        public void ShowLoadingScreen()
        {
            _loadingBackground.gameObject.SetActive(true);
            ShowLoadingIcon();
        }

        public void HideLoadingScreen()
        {
            HideLoadingIcon();
            _loadingBackground.gameObject.SetActive(false);
        }
        
        public void ShowLoadingIcon()
        {
            _loadingIcon.gameObject.SetActive(true);
        }

        public void HideLoadingIcon()
        {
            _loadingIcon.gameObject.SetActive(false);
        }
    }
}