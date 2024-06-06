using Managers;
using UnityEngine;
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