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
        private FlowerInstance _current;
        private Environment _currentEnv;
        private PlantRenderer _pr;

        #region SetUp

        private void Awake()
        {
            _pr = GetComponent<PlantRenderer>();
        }

        public void SetActive(int index)
        {
            gameObject.SetActive(true);    
            
            if(_current is not null) OnDisable();

            WateringCan.OnWatering += WaterPlant;
            
            Tuple<FlowerInstance, Environment> tmp = FlowerDisplay.Instance.GetFlowerAndEnv(index);
            _current = tmp.Item1;
            _currentEnv = tmp.Item2;
            _current.OnChange += RefreshVisualsWrapper;
            _pr.RefreshVisuals(_current, _currentEnv);
        }

        private void OnDisable()
        {
            WateringCan.OnWatering -= WaterPlant;
            
            if(_current is not null) _current.OnChange -= RefreshVisualsWrapper;
            _current = null;
            _currentEnv = new Environment();
            _pr.DebugClearRender();
        }

        #endregion

        #region Care

        private void WaterPlant()
        {
            Debug.Assert(_current is not null, "Illegal Event Subscription");
            _current.Water();
        }

        public void Replant(FlowerInstance flower, Environment env)
        {
            gameObject.SetActive(true);
            OnDisable();
            
            _current = flower;
            _currentEnv = env;
            _current.OnChange += RefreshVisualsWrapper;
            _pr.RefreshVisuals(_current, env);
            
            ShelfFertilizerItem.OnFertilizer += Fertilize;
            CInputManager.Instance.SetNavigation(false);
        }
        
        private void Fertilize(Environment.FertilizerType type)
        {
            CInputManager.Instance.SetNavigation(true);
            ShelfFertilizerItem.OnFertilizer -= Fertilize;
            WateringCan.OnWatering += WaterPlant;
            
            _current.Replant(type);
        }

        #endregion

        #region Util

        private void RefreshVisualsWrapper() => _pr.RefreshVisuals(_current, _currentEnv);

        #endregion
    }
}