using Data;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Clickable
{
    public abstract class ShelfSeedsItem : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private FlowerData seedsType;

        public delegate void ShelfItemClicked(FlowerData type);
        public static ShelfItemClicked OnSeedClicked;
        
        public void OnPointerClick(PointerEventData eventData)
        {
           OnSeedClicked?.Invoke(seedsType); 
        }
    }
}