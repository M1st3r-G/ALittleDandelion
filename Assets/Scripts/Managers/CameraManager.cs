using Clickable;
using UnityEngine;

namespace Managers
{
    public class CameraManager : MonoBehaviour
    {
        // Component References
        [Header("Components")]
        [SerializeField] private Camera flowerCam;
        [SerializeField] private Camera overview;
        [SerializeField] private Greenhouse greenhouseClick;
        
        public bool IsInGreenhouse => flowerCam.gameObject.activeSelf;
        public static CameraManager Instance { get; private set; }

        #region SetUp

        private void Awake()
        {
            Instance = this;
            ToHub();
        }

        private void OnDestroy()
        {
            if (Instance == this) Instance = null;
        }

        #endregion

        public void ToHub()
        {
            if (!flowerCam.gameObject.activeSelf) return;

            CInputManager.Instance.ShowBookButton(false);
            
            flowerCam.gameObject.SetActive(false);
            overview.gameObject.SetActive(true);
            
            greenhouseClick.enabled = true;
        }
        
        public void ToGreenhouse()
        {
            if (flowerCam.gameObject.activeSelf) return;
            
            CInputManager.Instance.ShowBookButton(true);
            
            flowerCam.gameObject.SetActive(true);
            overview.gameObject.SetActive(false);
            
            greenhouseClick.enabled = false;
            greenhouseClick.transform.GetChild(greenhouseClick.transform.childCount - 1).gameObject.SetActive(false);
        }
    }
}
