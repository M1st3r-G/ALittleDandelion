using UnityEngine;

namespace UI
{
    [RequireComponent(typeof(CanvasGroup))]
    public class PauseMenuController : MonoBehaviour
    {
        private CanvasGroup _cg;

        public static PauseMenuController Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
            
            _cg = GetComponent<CanvasGroup>();
            _cg.interactable = _cg.blocksRaycasts = false;
            _cg.alpha = 0;
        }

        private void OnDestroy()
        {
            if (Instance == this) Instance = null;
        }

        public void Toggle()
        {
            bool targetState = _cg.alpha < 0.5f;

            _cg.interactable = _cg.blocksRaycasts = targetState;
            _cg.alpha = targetState ? 1 : 0;
        }
    }
}