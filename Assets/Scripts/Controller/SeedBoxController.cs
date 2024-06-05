using Data;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Controller
{
    [RequireComponent(typeof(EventTrigger))]
    public class SeedBoxController : MonoBehaviour, IPointerClickHandler
    {
        public FlowerInstance Flower
        {
            set
            {
                _flower = value;
                Display();
            }
            get => _flower;
        }
        private FlowerInstance _flower;
        private Environment _tmp;
        
        private void Display()
        {
            Debug.LogWarning($"{_flower}");
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            Debug.Log("SeedBox was Clicked");
        }
    }
}