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

        private readonly Vector3 _hidePosition = new(-4.65f, -5.5f, -1f);
        private readonly Vector3 _defaultPosition = new(0,0,0.56f);
        
        #region SetUp
        public void CustomSetActive(bool state)
        {
            if (state)
            {
                transform.localPosition = _defaultPosition;
                CustomOnEnable();
            }
            else
            {
                transform.localPosition = _hidePosition;
                CustomOnDisable();
            }
        }
        
        private void CustomOnEnable()
        {
            ShelfSeedsItem.OnSeedClicked += OnSeedClicked;
            ShelfSoilItem.OnSoilClicked += OnSoilClicked;
            WateringCan.OnWatering += OnWatering;
            Shovel.OnLightTypeChange += OnShovelClicked;
            ReplantPot.OnReplant += OnReplant;
            
            TutorialManager.Instance.SetFlag(TutorialManager.TutorialFlag.FocusedSeed);
            
            foreach (SeedBoxController box in _allBoxes) box.SetHover(true);
        }

        private void CustomOnDisable()
        {
            ShelfSeedsItem.OnSeedClicked -= OnSeedClicked;
            ShelfSoilItem.OnSoilClicked -= OnSoilClicked;
            WateringCan.OnWatering -= OnWatering;
            Shovel.OnLightTypeChange -= OnShovelClicked;
            ReplantPot.OnReplant -= OnReplant;
            
            if(_currentSelection is not null) _currentSelection.Deselect();
            _currentSelection = null;

            foreach (SeedBoxController box in _allBoxes) box.SetHover(false);
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

            AudioManager.Instance.PlayEffect(AudioManager.AudioEffect.SeedPlant);
            TutorialManager.Instance.SetFlag(TutorialManager.TutorialFlag.PlantedSeed);
            
            _currentSelection.AddFlower(type);
        }

        private void OnSoilClicked(Environment.SoilType type)
        {
            Debug.Log("Noticed Soil Event");
            if (_currentSelection is null) return;
            if (!_currentSelection.IsEditable) return;

            AudioManager.Instance.PlayEffect(AudioManager.AudioEffect.Dirt);
            _currentSelection.AddSoil(type);
        }

        private void OnShovelClicked()
        {
            Debug.Log("Noticed Shovel Event");
            if (_currentSelection is null) return;
            if (!_currentSelection.IsEditable) return;
            if (!_currentSelection.EnvironmentIsSet) return;

            AudioManager.Instance.PlayEffect(AudioManager.AudioEffect.Shovel);
            _currentSelection.ChangeLightType();
        }

        private void OnWatering()
        {
            if (_currentSelection is null) return;
            if (_currentSelection.IsEditable) return;

            AudioManager.Instance.PlayEffect(AudioManager.AudioEffect.Water);
            TutorialManager.Instance.SetFlag(TutorialManager.TutorialFlag.Watered);
            _currentSelection.WaterPlant();
        }

        private void OnReplant()
        {
            if (_currentSelection is null) return;
            if (_currentSelection.IsEditable) return;
            if (!_currentSelection.IsReplantable) return;
            if (!FlowerInstanceLibrary.Instance.HasSpace) return;

            AudioManager.Instance.PlayEffect(AudioManager.AudioEffect.Replant);
            TutorialManager.Instance.SetFlag(TutorialManager.TutorialFlag.Replant);
            _currentSelection.Replant(); 
        }
        
        #endregion

        #region SelectionManagement

        public void BoxWasClicked(SeedBoxController box)
        {
            if (_currentSelection is not null) _currentSelection.Deselect();
            _currentSelection = box;
            box.Select();
            if(_currentSelection.IsDead) PlantRemoval.Instance.WaitForRemoval(true,0 , RemoveCurrentSelection);
        }

        private void RemoveCurrentSelection()
        {
            AudioManager.Instance.PlayEffect(AudioManager.AudioEffect.RemovePlant);
            _currentSelection.RemovePlant();
            _currentSelection = null;
        }
        
        #endregion

        #region Utils

        public void GetSaveContent(out FlowerInstance.FlowerSerialization[] flowers, out Environment[] environments, out int map)
        {
            List<FlowerInstance.FlowerSerialization> tmpFlowers = new(); 
            List<Environment> tmpEnvironments = new();
            
            map = 0;
            int copyMapValue = 1;
            foreach (Tuple<FlowerInstance, Environment> tuple in _allBoxes.Select(b => b.GetSaveContent()))
            {
                if (tuple.Item1 is not null)
                {
                    tmpFlowers.Add(tuple.Item1.Serialization);
                    tmpEnvironments.Add(tuple.Item2);
                    map += copyMapValue;
                }

                copyMapValue <<= 1;
            }

            flowers = tmpFlowers.ToArray();
            environments = tmpEnvironments.ToArray();
        }
        
        #endregion
    }
}