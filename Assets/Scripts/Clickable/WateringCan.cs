using UnityEngine;
using UnityEngine.EventSystems;

namespace Clickable
{
    public class WateringCan : MonoBehaviour, IPointerClickHandler
    {
        public delegate void WateringCanEvent();
        public static WateringCanEvent OnWatering;
        
        public void OnPointerClick(PointerEventData eventData)
        {
            OnWatering?.Invoke();
        }
    }
}