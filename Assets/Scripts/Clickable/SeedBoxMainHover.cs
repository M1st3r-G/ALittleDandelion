using Controller;

namespace Clickable
{
    public class SeedBoxMainHover: ClickableBase
    {
        public override void OnClick()
        {
            TableController.Instance.PlaceSeeds();
        }

        public void SetActive(bool state)
            => EnableHoverAndClick(state);
    }
}