using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class PotButtons : MonoBehaviour
    {
        [SerializeField] private Button[] buttons;
        
        public void SetAmountActive(bool[] states)
        {
            for (int i = 0; i < states.Length; i++) buttons[i].gameObject.SetActive(states[i]);
        }

        public void SetIndexToState(int index, bool state)
        {
            buttons[index].gameObject.SetActive(state);
        }
    }
}