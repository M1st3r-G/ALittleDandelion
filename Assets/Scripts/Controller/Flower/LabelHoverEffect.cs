using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Controller
{
    [RequireComponent(typeof(Collider))]
    public class LabelHoverEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        private TextMeshProUGUI _textField;
        private TextMeshProUGUI _labelText;

        private void Awake()
        {
            _textField = GameObject.FindGameObjectWithTag("LabelText").GetComponent<TextMeshProUGUI>();
            _labelText = GetComponentInChildren<TextMeshProUGUI>();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _textField.text = _labelText.text;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _textField.text = "";
        }
    }
}