using Data;
using UnityEngine;

namespace Clickable.Shelf
{
    public class ShelfSeedsItem : ClickableBase
    {
        [SerializeField] private FlowerData seedsType;

        public delegate void ShelfItemClicked(FlowerData type);
        public static ShelfItemClicked OnSeedClicked;
        
        public override void OnClick()
        {
            Debug.Log($"ShelfItemSeed was Clicked: {seedsType}");
           OnSeedClicked?.Invoke(seedsType); 
        }
    }
}