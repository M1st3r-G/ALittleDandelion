using Clickable;
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
            TimeManager.OnTimeIncrease += ClearSelection;
            ShelfSeedsItem.OnSeedClicked += OnSeedClicked;
            ShelfSoilItem.OnSoilClicked += OnSoilClicked;
        }

        private void OnDisable()
        {
            TimeManager.OnTimeIncrease -= ClearSelection;
            ShelfSeedsItem.OnSeedClicked -= OnSeedClicked;
            ShelfSoilItem.OnSoilClicked -= OnSoilClicked;
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

        #endregion

        #region SelectionManagement

        public void BoxWasClicked(SeedBoxController box)
        {
            Debug.Log("SeedBox was Clicked");
            if (_currentSelection is not null) _currentSelection.Deselect();
            _currentSelection = box;
            box.Select();
        }

        private void ClearSelection()
        {
            if(_currentSelection is not null) _currentSelection.Deselect();
            _currentSelection = null;
        }

        #endregion
    }
}