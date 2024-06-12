using System;
using System.Linq;
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
        private SeedBoxController _currentSelection;

        #region SetUp

        private void OnEnable()
        {
            ShelfSeedsItem.OnSeedClicked += OnSeedClicked;
            ShelfSoilItem.OnSoilClicked += OnSoilClicked;
            WateringCan.OnWatering += OnWatering;
            Shovel.OnLightTypeChange += OnShovelClicked;
            ReplantPot.OnReplant += OnReplant;
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
            if (!FlowerInstanceLibrary.Instance.HasSpace) return;
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

        #region Utils

        public void GetSaveContent(out FlowerInstance.FlowerSerialization[] flowers, out Environment[] environments)
        {
            var boxes = GetComponentsInChildren<SeedBoxController>();
            flowers = new FlowerInstance.FlowerSerialization[6]; 
            environments = new Environment[6];
            int c = 0;
            foreach (Tuple<FlowerInstance, Environment> tuple in boxes.Select(b => b.GetSaveContent()))
            {
                if (tuple.Item1 is null) flowers[c] = null;
                else flowers[c] = tuple.Item1.Serialization;
                
                environments[c] = tuple.Item2;
                c++;
            }
        }
        
        #endregion
    }
}