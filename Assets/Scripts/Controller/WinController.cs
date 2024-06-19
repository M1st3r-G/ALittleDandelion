using UnityEngine;

namespace Controller
{
    public class WinController : MonoBehaviour
    {
        private bool _wasTriggered;

        private void Awake()
        {
            gameObject.SetActive(false);
        }

        public void Show()
        {
            if (_wasTriggered) return;

            _wasTriggered = true;
            gameObject.SetActive(true);
        }

        public void Hide() => gameObject.SetActive(false);
    }
}