using Managers;
using UnityEditor.Rendering;
using UnityEngine.EventSystems;

namespace Clickable
{
    public class Greenhouse : ClickableBase
    {
        //ComponentReferences
        //Params
        //Temps
        //Public
        
        public override void OnPointerClick(PointerEventData eventData)
        {
            CameraManager.Instance.ToGreenhouse();
        }
    }
}