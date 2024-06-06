using UnityEngine.EventSystems;

namespace Clickable
{
    public class Shovel : ClickableBase
    {
        public delegate void ShovelLightChangeEvent();
        public static ShovelLightChangeEvent OnLightTypeChange;
        
        public override void OnPointerClick(PointerEventData eventData)
        {
            OnLightTypeChange?.Invoke();
        }
    }
}