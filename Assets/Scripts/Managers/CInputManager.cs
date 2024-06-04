using UnityEngine;
using UnityEngine.InputSystem;

namespace Managers
{
    public class CInputManager : MonoBehaviour
    {
        // Component References
        [SerializeField] private InputActionReference showFlowerAction;
        [SerializeField] private InputActionReference escapeAction;

        public static CInputManager Instance { get; private set; }

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
        
            escapeAction.action.Enable();
            escapeAction.action.performed += Escape;
        }

    
        private void OnDisable()
        {
            showFlowerAction.action.performed -= ShowFlowerWrapper;
            showFlowerAction.action.Disable();
        
            escapeAction.action.Disable();
            escapeAction.action.performed -= Escape;
        }

        #endregion

        private void Escape(InputAction.CallbackContext ctx)
        {
            if (CameraManager.Instance.IsInGreenhouse) CameraManager.Instance.ToHub();
            else Debug.Log("Opend Settings");
        }

        private void ShowFlowerWrapper(InputAction.CallbackContext ctx)
            => ShowFlowerAtIndex((int)ctx.ReadValue<float>() - 1);
        
        public void ShowFlowerAtIndex(int index)
        {
            CameraManager.Instance.ToGreenhouse();
            Debug.Log($"Flower with index: {index}");
        }
        
        #region Buttons

        public void TriggerNextDay()
        {
            Debug.Log("Next Day");
        }

        public void Settings()
        {
            Debug.Log("Settings");
        }

        public void Seeds()
        {
            Debug.Log("Seeds");
        }

        public void Book() => Debug.Log("Open Book");
    
        public void OnPotClicked(int index)
        {
            ShowFlowerAtIndex(index);
        }
        
        #endregion
    }
}