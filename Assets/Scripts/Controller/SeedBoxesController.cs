using Clickable;
using Clickable.Shelf;
using Data;
using UnityEngine;
using Environment = Data.Environment;

namespace Controller
{
    public class SeedBoxesController : MonoBehaviour
    {
        private SeedBoxController _currentSelection;

        #region SetUp

        private void OnEnable()
        {
            ShelfSeedsItem.OnSeedClicked += OnSeedClicked;
            ShelfSoilItem.OnSoilClicked += OnSoilClicked;
            WateringCan.OnWatering += OnWatering;
            Shovel.OnLightTypeChange += OnShovelClicked;
        }

        private void OnDisable()
        {
            ShelfSeedsItem.OnSeedClicked -= OnSeedClicked;
            ShelfSoilItem.OnSoilClicked -= OnSoilClicked;
            WateringCan.OnWatering -= OnWatering;
            Shovel.OnLightTypeChange -= OnShovelClicked;
            
            if(_currentSelection is not null) _currentSelection.Deselect();
            _currentSelection = null;
        }
        
        #endregion

        #region ShelfInteraction

        private void OnSeedClicked(FlowerData type)
        {
            Debug.Log("Noticed Seed Event");
            if (_currentSelection is null) return;
            if (!_currentSelection.IsEditable) return;

            _currentSelection.AddFlower(type);
        }

        private void OnSoilClicked(Environment.SoilType type)
        {
            Debug.Log("Noticed Soil Event");
            if (_currentSelection is null) return;
            if (!_currentSelection.IsEditable) return;

            _currentSelection.AddSoil(type);
        }

        private void OnShovelClicked()
        {
            Debug.Log("Noticed Shovel Event");
            if (_currentSelection is null) return;
            if (!_currentSelection.IsEditable) return;
            
            _currentSelection.ChangeLightType();
        }

        private void OnWatering()
        {
            if (_currentSelection is null) return;
            if (_currentSelection.IsEditable) return;
            _currentSelection.WaterPlant();
        }
        
        #endregion

        #region SelectionManagement

        public void BoxWasClicked(SeedBoxController box)
        {
            if (_currentSelection is not null) _currentSelection.Deselect();
            _currentSelection = box;
            box.Select();
        }

        #endregion
    }
}