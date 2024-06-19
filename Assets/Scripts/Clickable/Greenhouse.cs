using Managers;

namespace Clickable
{
    public class Greenhouse : ClickableBase
    {
        //ComponentReferences
        //Params
        //Temps
        //Public
        
        public override void OnClick()
        {
            CameraManager.Instance.ToGreenhouse();
            TutorialManager.Instance.SetFlag(TutorialManager.TutorialFlag.EnterGreenhouse);
        }

        public void Enable() => EnableHoverAndClick(true);
        public void Disable() => EnableHoverAndClick(false);
    }
}