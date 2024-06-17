using Data;
using UnityEngine;

namespace Clickable.Shelf
{
    public class ShelfFertilizerItem : ClickableBase
    {
        [SerializeField] private Environment.FertilizerType type;

        public delegate void OnFertilizerClickedEvent(Environment.FertilizerType type);
        public static OnFertilizerClickedEvent OnFertilizer;
        
        public override void OnClick()
        {
            OnFertilizer?.Invoke(type);
        }
    }
}