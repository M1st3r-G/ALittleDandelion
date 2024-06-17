namespace Clickable.Shelf
{
    public class ReplantPot : ClickableBase
    {
        public delegate void ReplantEvent();
        public static ReplantEvent OnReplant;
        
        public override void OnClick()
        {
            OnReplant?.Invoke();
        }
    }
}