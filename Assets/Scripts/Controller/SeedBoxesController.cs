using System;
using System.Collections.Generic;
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
        private SeedBoxController[] _allBoxes;
        
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

        private void Awake()
        {
            _allBoxes = GetComponentsInChildren<SeedBoxController>();
        }

        private void Start()
        {
            SaveGameManager.Instance.GetSeedBoxData(out FlowerInstance[] flowers, out Environment[] envs);

            for (var i = 0; i < _allBoxes.Length; i++)
            {
                _allBoxes[i].SetUpContent(flowers[i], envs[i]);
            }
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

        public void GetSaveContent(out FlowerInstance.FlowerSerialization[] flowers, out Environment[] environments, out int map)
        {
            
            List<FlowerInstance.FlowerSerialization> tmpFlowers = new(); 
            List<Environment> tmpEnvironments = new();
            
            map = 0;
            int copyMapValue = 1 << 5;
            foreach (Tuple<FlowerInstance, Environment> tuple in _allBoxes.Select(b => b.GetSaveContent()))
            {
                if (tuple.Item1 is not null)
                {
                    tmpFlowers.Add(tuple.Item1.Serialization);
                    tmpEnvironments.Add(tuple.Item2);
                    map += copyMapValue;
                }

                copyMapValue >>= 1;
            }

            flowers = tmpFlowers.ToArray();
            environments = tmpEnvironments.ToArray();
        }
        
        #endregion
    }
}