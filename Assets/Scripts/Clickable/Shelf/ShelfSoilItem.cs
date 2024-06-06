using Data;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Clickable.Shelf
{
    public class ShelfSoilItem : ClickableBase
    {
        [SerializeField] private Environment.SoilType soilType;

        public delegate void ShelfItemClicked(Environment.SoilType type);
        public static ShelfItemClicked OnSoilClicked;
        
        public override void OnPointerClick(PointerEventData eventData)
        {
            Debug.Log($"ShelfItemSoil was Clicked: {soilType}");
            OnSoilClicked?.Invoke(soilType); 
        }
    }
}