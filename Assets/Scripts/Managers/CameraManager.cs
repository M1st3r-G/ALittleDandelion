using Clickable;
using Controller.Book;
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

        [Header("BlumenBuch")] 
        [SerializeField] private Vector3 positionOffset;
        [SerializeField] private Vector3 rotation;
        [SerializeField] private PageController book;
        [SerializeField] private float outOfFrameX = 12f;
        
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
            flowerCam.gameObject.SetActive(false);
            greenhouseClick.enabled = true;
            overview.gameObject.SetActive(true);
        }
        
        public void ToGreenhouse()
        {
            if (flowerCam.gameObject.activeSelf) return;
            flowerCam.gameObject.SetActive(true);
            SetBookTo(flowerCam.transform);
            greenhouseClick.enabled = false;
            greenhouseClick.transform.GetChild(greenhouseClick.transform.childCount - 1).gameObject.SetActive(false);
            overview.gameObject.SetActive(false);
        }

        private void SetBookTo(Transform target)
        {
            book.transform.SetParent(target);
            book.transform.SetLocalPositionAndRotation(positionOffset, Quaternion.Euler(rotation));
        }
    }
}
