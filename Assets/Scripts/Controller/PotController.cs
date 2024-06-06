using Clickable;
using Clickable.Shelf;
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

        public void Replant(FlowerInstance flower)
        {
            gameObject.SetActive(true);
            
            OnDisable();
            _current = flower;

            ShelfFertilizerItem.OnFertilizer += Fertilize;
            CInputManager.Instance.SetNavigation(false);
        }
            
        private void Fertilize(Environment.FertilizerType type)
        {
            CInputManager.Instance.SetNavigation(true);
            
                        
            TimeManager.OnTimeIncrease += RefreshVisuals;
            WateringCan.OnWatering += WaterPlant;
            
            ShelfFertilizerItem.OnFertilizer -= Fertilize;
            _current.Replant(type);
            RefreshVisuals();
        }
    
        
        private void RefreshVisuals()
        {
            debugDisplay.text = _current.ToString();
        }
    }
}