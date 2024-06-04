using UnityEngine;
using UnityEngine.InputSystem;

namespace Managers
{
    public class CInputManager : MonoBehaviour
    {
        // Component References
        [SerializeField] private InputActionReference showFlowerAction;
        [SerializeField] private InputActionReference showSeedsAction;
        [SerializeField] private InputActionReference showShelfedAction;
        [SerializeField] private InputActionReference escapeAction;
        [SerializeField] private InputActionReference bookAction;
        
        private static CInputManager Instance { get; set; }

        #region SetUp

        private void Awake()
        {
            Instance = this;
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

        private void ShowShelfedWrapper(InputAction.CallbackContext ctx) => Debug.Log("Show Shelfed");
        
        public void TriggerNextDay() => TimeManager.Instance.NextDay();

        private void Escape(InputAction.CallbackContext ctx)
        {
            if (CameraManager.Instance.IsInGreenhouse) CameraManager.Instance.ToHub();
            else Settings();
        }
        public void Settings() => Debug.Log("Settings");

        private void ShowFlowerWrapper(InputAction.CallbackContext ctx)
            => ShowFlower((int)ctx.ReadValue<float>() - 1);

        public void ShowFlower(int index)
        {
            CameraManager.Instance.ToGreenhouse();
            Debug.Log($"Flower with index: {index}");
        }
        
        private void SeedsWrapper(InputAction.CallbackContext ctx) => Seeds();
        public void Seeds() => Debug.Log("Seeds");

        private void BookWrapper(InputAction.CallbackContext ctx) => Book();
        public void Book() => Debug.Log("Open Book");
    }
}