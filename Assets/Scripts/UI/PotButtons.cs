using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class PotButtons : MonoBehaviour
    {
        private Button[] _buttons;
        
        private void Awake()
        {
            _buttons = GetComponentsInChildren<Button>();

            foreach (Button b in _buttons) b.gameObject.SetActive(false);
        }

        public void SetAmountActive(bool[] states)
        {
            // Copy States
            for (int i = 0; i < states.Length; i++) _buttons[i].gameObject.SetActive(states[i]);
        }
    }
}