using UnityEngine;
using UnityEngine.EventSystems;

namespace Clickable
{
    public class WateringCan : ClickableBase
    {
        public delegate void WateringCanEvent();
        public static WateringCanEvent OnWatering;
        
        public override void OnPointerClick(PointerEventData eventData)
        {
            OnWatering?.Invoke();
        }
    }
}