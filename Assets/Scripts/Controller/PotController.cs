using Clickable;
using Data;
using Managers;
using TMPro;
using UnityEngine;

namespace Controller
{
    public class PotController : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI debugDisplay;
        private FlowerInstance _current;
        
        public void SetActive(int index)
        {
            gameObject.SetActive(true);    
            
            if(_current is not null) OnDisable();

            TimeManager.OnTimeIncrease += RefreshVisuals;
            WateringCan.OnWatering += WaterPlant;
            
            _current = FlowerDisplay.Instance.GetFlower(index);
            RefreshVisuals();
        }

        private void OnDisable()
        {
            _current = null;
            TimeManager.OnTimeIncrease -= RefreshVisuals;
            WateringCan.OnWatering -= WaterPlant;
            debugDisplay.text = "";
        }

        private void WaterPlant()
        {
            Debug.Assert(_current is not null, "Illegal Event Subscription");
            _current.Water();
            RefreshVisuals();
        }
        
        private void RefreshVisuals()
        {
            debugDisplay.text = _current.ToString();
        }
    }
}