using Clickable;
using Clickable.Shelf;
using Data;
using Managers;
using UnityEngine;
using Environment = Data.Environment;

namespace Controller
{
    public class SeedBoxesController : MonoBehaviour
    {
        private SeedBoxController[] _allBoxes;
        private SeedBoxController _currentSelection;

        #region SetUp

        private void Awake()
        {
            _allBoxes = GetComponentsInChildren<SeedBoxController>(true);
        }

        private void OnEnable()
        {
            ShelfSeedsItem.OnSeedClicked += OnSeedClicked;
            ShelfSoilItem.OnSoilClicked += OnSoilClicked;
            WateringCan.OnWatering += OnWatering;
            Shovel.OnLightTypeChange += OnShovelClicked;
            ReplantPot.OnReplant += OnReplant;

            foreach (SeedBoxController box in _allBoxes) box.SetRendererActive(true);
        }

        private void OnDisable()
        {
            ShelfSeedsItem.OnSeedClicked -= OnSeedClicked;
            ShelfSoilItem.OnSoilClicked -= OnSoilClicked;
            WateringCan.OnWatering -= OnWatering;
            Shovel.OnLightTypeChange -= OnShovelClicked;
            ReplantPot.OnReplant -= OnReplant;
            
            if(_currentSelection is not null) _currentSelection.Deselect();
            _currentSelection = null;
            
            foreach (SeedBoxController box in _allBoxes) box.SetRendererActive(false);
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
            if (!_currentSelection.EnvironmentIsSet) return;
            _currentSelection.ChangeLightType();
        }

        private void OnWatering()
        {
            if (_currentSelection is null) return;
            if (_currentSelection.IsEditable) return;
            _currentSelection.WaterPlant();
        }

        private void OnReplant()
        {
            if (_currentSelection is null) return;
            if (_currentSelection.IsEditable) return;
            if (!_currentSelection.IsReplantable) return;
            if (!FlowerDisplay.Instance.HasSpace) return;
            _currentSelection.Replant(); 
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