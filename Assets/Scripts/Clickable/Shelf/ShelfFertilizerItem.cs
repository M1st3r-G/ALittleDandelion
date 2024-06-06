using Data;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Clickable.Shelf
{
    public class ShelfFertilizerItem : ClickableBase
    {
        [SerializeField] private Environment.FertilizerType type;

        public delegate void OnFertilizerClickedEvent(Environment.FertilizerType type);
        public static OnFertilizerClickedEvent OnFertilizer;
        
        public override void OnPointerClick(PointerEventData eventData)
        {
            OnFertilizer?.Invoke(type);
        }
    }
}