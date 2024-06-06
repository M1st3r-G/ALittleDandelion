using System.Linq;
using Clickable;
using Data;
using Managers;
using UnityEngine;

namespace Controller
{
    public class SeedBoxesController : MonoBehaviour
    {
        private SeedBoxController[] _boxes;
        private SeedBoxController _currentSelection;
        
        private void Awake()
        {
            _boxes = GetComponentsInChildren<SeedBoxController>();
        }

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
        
        private void Start() => FetchFlowers();
        
        private void FetchFlowers()
        {
            ShelfSeedsItem.OnSeedClicked += OnSeedClicked;
            ShelfSoilItem.OnSoilClicked += OnSoilClicked;
            
            // Refreshes Display with data from Storage   
            FlowerInstance[] flowers = Storage.Instance.GetSeeds();
            for (int i = 0; i < flowers.Length; i++)
            {
                _boxes[i].Flower = flowers[i];
            }
        }

        private void SaveFlowers()
        {
            Storage.Instance.StoreSeeds(_boxes.Select(b => b.Flower).ToArray());
            ShelfSeedsItem.OnSeedClicked -= OnSeedClicked;
            ShelfSoilItem.OnSoilClicked -= OnSoilClicked;
        }

        public void BoxWasClicked(SeedBoxController box)
        {
            Debug.Log("SeedBox was Clicked");
            if (_currentSelection is not null) _currentSelection.Deselect();
            _currentSelection = box;
        }
    }
}