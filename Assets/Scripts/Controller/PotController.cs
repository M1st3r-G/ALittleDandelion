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

            WateringCan.OnWatering += WaterPlant;
            
            _current = FlowerDisplay.Instance.GetFlower(index);
            _current.OnChange += RefreshVisuals;
            RefreshVisuals();
        }

        private void OnDisable()
        {
            WateringCan.OnWatering -= WaterPlant;
            
            if(_current is not null) _current.OnChange -= RefreshVisuals;
            _current = null;
            debugDisplay.text = "";
        }

        private void WaterPlant()
        {
            Debug.Assert(_current is not null, "Illegal Event Subscription");
            _current.Water();
        }

        public void Replant(FlowerInstance flower)
        {
            gameObject.SetActive(true);
            
            OnDisable();
            _current = flower;
            _current.OnChange += RefreshVisuals;
            RefreshVisuals();
            
            ShelfFertilizerItem.OnFertilizer += Fertilize;
            CInputManager.Instance.SetNavigation(false);
        }
            
        private void Fertilize(Environment.FertilizerType type)
        {
            CInputManager.Instance.SetNavigation(true);
            WateringCan.OnWatering += WaterPlant;
            
            ShelfFertilizerItem.OnFertilizer -= Fertilize;
            _current.Replant(type);
        }
        
        private void RefreshVisuals()
        {
            debugDisplay.text = _current.ToString();
        }
    }
}