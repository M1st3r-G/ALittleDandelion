using System;
using Clickable;
using Clickable.Shelf;
using Data;
using Managers;
using UnityEngine;
using Environment = Data.Environment;

namespace Controller
{
    [RequireComponent(typeof(PlantRenderer))]
    public class PotController : MonoBehaviour
    {
        private PlantRenderer _pr;
        private FlowerInstance _currentFlower;
        private Environment _currentEnv;

        private bool _isDisplayedOnTable; // For removen on Grow
        
        #region SetUp

        private void Awake()
        {
            _pr = GetComponent<PlantRenderer>();
        }

        public void SetUpContent(FlowerInstance flower, Environment env)
        {
            if (flower is null) return;
            
            gameObject.SetActive(true);
            
            _currentFlower = flower;
            _currentEnv = env;
            
            _currentFlower.OnChange += RefreshVisualsWrapper;
            _pr.RefreshVisuals(_currentFlower, _currentEnv);
        }

        #endregion

        #region DisplayManagement

        public void SetDisplayed(bool state)
        {
            _isDisplayedOnTable = state;
            if (state) WateringCan.OnWatering += WaterPlant;
            else WateringCan.OnWatering -= WaterPlant;
        }

        #endregion
        
        #region Care

        private void WaterPlant()
        {
            Debug.Assert(_currentFlower is not null, "Illegal Event Subscription");
            _currentFlower.Water();
        }

        public void Replant(FlowerInstance flower, Environment env)
        {
            TableController.Instance.CenterPot(this);
            
            gameObject.SetActive(true);
            
            _currentFlower = flower;
            _currentEnv = env;

            _isDisplayedOnTable = true;
            
            _currentFlower.OnChange += RefreshVisualsWrapper;
            _pr.RefreshVisuals(_currentFlower, _currentEnv);
            
            ShelfFertilizerItem.OnFertilizer += Fertilize;
            CInputManager.Instance.SetNavigation(false);
        }
        
        private void Fertilize(Environment.FertilizerType type)
        {
            CInputManager.Instance.SetNavigation(true);
            ShelfFertilizerItem.OnFertilizer -= Fertilize;
            WateringCan.OnWatering += WaterPlant;
            
            _currentFlower.Replant(type);
        }

        public void Empty()
        {
            WateringCan.OnWatering -= WaterPlant;
            _currentFlower.OnChange -= RefreshVisualsWrapper;
            
            _currentFlower = null;
            _currentEnv = default;
            
            _pr.RefreshVisuals(_currentFlower, _currentEnv);
            
            gameObject.SetActive(false);
        }
        
        #endregion

        #region Util

        private void RefreshVisualsWrapper() => _pr.RefreshVisuals(_currentFlower, _currentEnv);
        public Tuple<FlowerInstance, Environment> GetSaveContent()
            => new(_currentFlower, _currentEnv);
        public bool IsDead => 
            _currentFlower is not null && _currentFlower.State == FlowerInstance.GrowthState.Dead;
        public bool IsFullyGrown =>
            _currentFlower is not null && _currentFlower.State == FlowerInstance.GrowthState.Flower;

        public int GetRatingOfFlower(out FlowerData.FlowerType type)
        {
            type = FlowerData.FlowerType.Löwenzahn;
            if (_currentFlower is null) return -1;

            type = _currentFlower.Type;
            return _currentFlower.CalculateStars();
        }
        
        #endregion
    }
}