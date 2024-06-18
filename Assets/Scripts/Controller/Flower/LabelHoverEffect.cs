using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Controller
{
    [RequireComponent(typeof(Collider))]
    public class LabelHoverEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        private TextMeshProUGUI _outputField;
        
        public bool flag;
        public TextMeshProUGUI labelText;
        public string contentToShow;
        
        private void Awake()
        {
            _outputField = GameObject.FindGameObjectWithTag("LabelText").GetComponent<TextMeshProUGUI>();
            labelText = GetComponentInChildren<TextMeshProUGUI>();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            string tmp = flag ? labelText.text : contentToShow;
            _outputField.text = tmp;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _outputField.text = "";
        }
    }
}