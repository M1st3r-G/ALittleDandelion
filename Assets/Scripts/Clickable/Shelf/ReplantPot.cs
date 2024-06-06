using UnityEngine.EventSystems;

namespace Clickable.Shelf
{
    public class ReplantPot : ClickableBase
    {
        public delegate void ReplantEvent();
        public static ReplantEvent OnReplant;
        
        public override void OnPointerClick(PointerEventData eventData)
        {
            OnReplant?.Invoke();
        }
    }
}