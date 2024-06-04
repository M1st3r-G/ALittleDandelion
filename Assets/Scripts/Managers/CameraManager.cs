using UnityEngine;

namespace Managers
{
    public class CameraManager : MonoBehaviour
    {
        // Component References
        [SerializeField] private Camera flowerCam;
        [SerializeField] private Camera overview;
        
        public bool IsInHub => !flowerCam.gameObject.activeSelf;
        public bool IsInGreenhouse => flowerCam.gameObject.activeSelf;
        public static CameraManager Instance { get; private set; }

        #region SetUp

        private void Awake()
        {
            Instance = this;
            ToGreenhouse();
        }

        private void OnDestroy()
        {
            if (Instance == this) Instance = null;
        }

        #endregion
        
        public void ToHub()
        {
            if (!flowerCam.gameObject.activeSelf) return;
            flowerCam.gameObject.SetActive(false);
            overview.gameObject.SetActive(true);
        }
        
        public void ToGreenhouse()
        {
            if (flowerCam.gameObject.activeSelf) return;
            flowerCam.gameObject.SetActive(true);
            overview.gameObject.SetActive(false);
        }
    }
}
