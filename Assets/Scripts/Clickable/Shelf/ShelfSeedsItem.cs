using Data;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Clickable.Shelf
{
    public class ShelfSeedsItem : ClickableBase
    {
        [SerializeField] private FlowerData seedsType;

        public delegate void ShelfItemClicked(FlowerData type);
        public static ShelfItemClicked OnSeedClicked;
        
        public override void OnPointerClick(PointerEventData eventData)
        {
            Debug.Log($"ShelfItemSeed was Clicked: {seedsType}");
           OnSeedClicked?.Invoke(seedsType); 
        }
    }
}