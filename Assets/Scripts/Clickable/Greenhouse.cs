using Managers;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Clickable
{
    public class Greenhouse : MonoBehaviour, IPointerClickHandler
    {
        //ComponentReferences
        //Params
        //Temps
        //Public

        public void OnPointerClick(PointerEventData eventData)
        {
            CameraManager.Instance.ToGreenhouse();
        }
    }
}