namespace Clickable
{
    public class WateringCan : ClickableBase
    {
        public delegate void WateringCanEvent();
        public static WateringCanEvent OnWatering;
        
        public override void OnClick()
        {
            OnWatering?.Invoke();
        }
    }
}