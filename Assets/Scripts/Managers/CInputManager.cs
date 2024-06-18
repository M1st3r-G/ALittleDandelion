using Controller;
using Controller.Book;
using UI;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace Managers
{
    public class CInputManager : MonoBehaviour
    {
        #region Fields

        // Component References
        [Header("InputActions")]
        [SerializeField] private InputActionReference showFlowerAction;
        [SerializeField] private InputActionReference showSeedsAction;
        [SerializeField] private InputActionReference showShelvedAction;
        [SerializeField] private InputActionReference escapeAction;
        [SerializeField] private InputActionReference bookAction;
        [SerializeField] private InputActionReference bookNavigationAction;

        [Header("ButtonReference")] 
        [SerializeField] private Button bookButton;
        
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
            showShelvedAction.action.Enable();
            showShelvedAction.action.performed += ShowShelvedWrapper;

            escapeAction.action.Enable();
            escapeAction.action.performed += Escape;
            bookAction.action.Enable();
            bookAction.action.performed += BookWrapper;
            bookNavigationAction.action.Enable();
            bookNavigationAction.action.performed += FlipPageWrapper;
        }
        
        private void OnDisable()
        {
            showFlowerAction.action.performed -= ShowFlowerWrapper;
            showFlowerAction.action.Disable();
            showSeedsAction.action.performed -= SeedsWrapper;
            showSeedsAction.action.Disable();
            showShelvedAction.action.performed -= ShowShelvedWrapper;
            showShelvedAction.action.Disable();
            
            escapeAction.action.performed -= Escape;
            escapeAction.action.Disable();
            bookAction.action.performed -= BookWrapper;
            bookAction.action.Disable();
            bookNavigationAction.action.performed -= FlipPageWrapper;
            bookNavigationAction.action.Disable();
        }

        #endregion

        #region General

        public void SetNavigation(bool state)
        {
            Debug.LogWarning((state ? "Enabled" : "Disabled") + "Navigation");
            _navigationActive = state;
        }

        public void PlayHoverSound() =>
            AudioManager.Instance.PlayEffect(AudioManager.AudioEffect.HoverUI);

        #endregion
        
        #region InputHandling

        private void ShowShelvedWrapper(InputAction.CallbackContext ctx) => Debug.Log("Show Shelved");
        
        public void TriggerNextDay() => TimeManager.Instance.NextDay();

        
        private void Escape(InputAction.CallbackContext ctx) => Settings();
        public void Settings()
        {
            if (!_navigationActive) return;

            AudioManager.Instance.PlayEffect(AudioManager.AudioEffect.Click);
            
            if (CameraManager.Instance.IsInGreenhouse) CameraManager.Instance.ToHub();
            else PauseMenuController.Instance.Toggle();
        }

        
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
            
            AudioManager.Instance.PlayEffect(AudioManager.AudioEffect.Click);
            
            CameraManager.Instance.ToGreenhouse();
            TableController.Instance.PlaceFlower(index - 1);
        }
        
        
        private void SeedsWrapper(InputAction.CallbackContext ctx) => Seeds();
        public void Seeds()
        {
            if (!_navigationActive) return;
            
            AudioManager.Instance.PlayEffect(AudioManager.AudioEffect.Click);
            CameraManager.Instance.ToGreenhouse();
            TableController.Instance.PlaceSeeds();
        }

        
        public void ToggleShowBookButton(bool show) => bookButton.gameObject.SetActive(show);
        private void BookWrapper(InputAction.CallbackContext ctx) => Book();
        public void Book()
        {
            if (!CameraManager.Instance.IsInGreenhouse) return;
            
            AudioManager.Instance.PlayEffect(AudioManager.AudioEffect.Click);
            BookController.Instance.ToggleBook();
        }

        
        private void FlipPageWrapper(InputAction.CallbackContext ctx) => FlipPage((int)ctx.ReadValue<float>());
        public  void FlipPage(int direction)
        {
            if (!_navigationActive) return;
            if (BookController.Instance is null) return; //Faul, vielleicht vorher Initialisieren.
            if(BookController.Instance.IsShown) BookController.Instance.FlipPage(direction);            
        }
        
        #endregion
    }
}