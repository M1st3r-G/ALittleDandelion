namespace Clickable
{
    public class Shovel : ClickableBase
    {
        public delegate void ShovelLightChangeEvent();
        public static ShovelLightChangeEvent OnLightTypeChange;
        
        public override void OnClick()
        {
            OnLightTypeChange?.Invoke();
        }
    }
}