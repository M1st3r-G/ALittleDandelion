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

            WateringCan.OnWatering += WaterPlant;
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

        #endregion

        #region Util

        private void RefreshVisualsWrapper() => _pr.RefreshVisuals(_currentFlower, _currentEnv);
        public Tuple<FlowerInstance, Environment> GetSaveContent()
            => new(_currentFlower, _currentEnv);

        #endregion
    }
}