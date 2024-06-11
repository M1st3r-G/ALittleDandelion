using Controller;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Managers
{
    public class CInputManager : MonoBehaviour
    {
        #region Fields

        // Component References
        [SerializeField] private InputActionReference showFlowerAction;
        [SerializeField] private InputActionReference showSeedsAction;
        [SerializeField] private InputActionReference showShelfedAction;
        [SerializeField] private InputActionReference escapeAction;
        [SerializeField] private InputActionReference bookAction;

        private bool _navigationActive;
        
        public static CInputManager Instance { get; private set; }
        

        #endregion
        
        #region SetUp

        private void Awake()
        {
            Instance = this;
            _navigationActive = true;
        }

        private void OnDestroy()
        {
            if (Instance == this) Instance = null;
        }

        private void OnEnable()
        {
            showFlowerAction.action.Enable();
            showFlowerAction.action.performed += ShowFlowerWrapper; 
            showSeedsAction.action.Enable();
            showSeedsAction.action.performed += SeedsWrapper;
            showShelfedAction.action.Enable();
            showShelfedAction.action.performed += ShowShelfedWrapper;

            escapeAction.action.Enable();
            escapeAction.action.performed += Escape;
            bookAction.action.Enable();
            bookAction.action.performed += BookWrapper;
        }
    
        private void OnDisable()
        {
            showFlowerAction.action.performed -= ShowFlowerWrapper;
            showFlowerAction.action.Disable();
            showSeedsAction.action.Disable();
            showSeedsAction.action.performed -= SeedsWrapper;
            showShelfedAction.action.Disable();
            showShelfedAction.action.performed -= ShowShelfedWrapper;
            
            escapeAction.action.Disable();
            escapeAction.action.performed -= Escape;
            bookAction.action.Disable();
            bookAction.action.performed -= BookWrapper;
        }

        #endregion

        public void SetNavigation(bool state)
        {
            Debug.LogWarning((state ? "Enabled" : "Disabled") + "Navigation");
            _navigationActive = state;
        }

        #region InputHandling

        private void ShowShelfedWrapper(InputAction.CallbackContext ctx) => Debug.Log("Show Shelfed");
        
        public void TriggerNextDay() => TimeManager.Instance.NextDay();

        private void Escape(InputAction.CallbackContext ctx)
        {
            if (!_navigationActive) return;
            
            if (CameraManager.Instance.IsInGreenhouse) CameraManager.Instance.ToHub();
            else Settings();
        }
        public void Settings() => Debug.Log("Settings");

        private void ShowFlowerWrapper(InputAction.CallbackContext ctx)
            => ShowFlower((int)ctx.ReadValue<float>());

        public void ShowFlower(int index)
        {
            if (!_navigationActive) return;
            if (!FlowerInstanceLibrary.Instance.FlowerIsSet(index - 1))
            {
                Debug.Log("FlowerInput Ignored, Plant is not set");
                return;
            }
            
            CameraManager.Instance.ToGreenhouse();
            TableController.Instance.PlaceFlower(index - 1);
        }
        
        private void SeedsWrapper(InputAction.CallbackContext ctx) => Seeds();
        public void Seeds()
        {
            if (!_navigationActive) return;
            
            TableController.Instance.PlaceSeeds();
        }

        private void BookWrapper(InputAction.CallbackContext ctx) => Book();
        public void Book() => Debug.Log("Open Book");

        #endregion
    }
}