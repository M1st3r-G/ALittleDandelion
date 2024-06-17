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
        }
    }
}