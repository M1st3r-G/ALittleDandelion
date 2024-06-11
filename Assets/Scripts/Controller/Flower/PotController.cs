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

        private void SetToFlower(FlowerInstance flower, Environment env)
        {
            gameObject.SetActive(true);
            
            _currentFlower = flower;
            _currentEnv = env;
            
            _currentFlower.OnChange += RefreshVisualsWrapper;
            _pr.RefreshVisuals(_currentFlower, _currentEnv);
        }

        private void RemoveFlower()
        {
            WateringCan.OnWatering -= WaterPlant;
            _currentFlower.OnChange -= RefreshVisualsWrapper;
            
            _currentFlower = null;
            _currentEnv = new Environment();
            
            _pr.DebugClearRender();
            gameObject.SetActive(false);
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
            SetToFlower(flower, env);
            
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

        #endregion
    }
}