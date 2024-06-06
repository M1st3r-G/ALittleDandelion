using System.Collections.Generic;
using Clickable.Shelf;
using Controller;
using Data;
using UI;
using UnityEngine;
using Environment = Data.Environment;

namespace Managers
{
    public class FlowerDisplay : MonoBehaviour
    {
        [SerializeField] private PotButtons potsDisplay;
        [SerializeField] private ReplantPot replantPot;
        
        public bool HasSpace => _flowers.Count < 12;
        private List<FlowerInstance> _flowers;
        
        public static FlowerDisplay Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
            _flowers = new List<FlowerInstance>();
        }
        
        private void OnDestroy()
        {
            if (Instance == this) Instance = null;
        }

        public void AddFlower(FlowerInstance f)
        {
            int index = _flowers.Count;
            _flowers.Add(f);
            potsDisplay.SetActive(_flowers.ToArray());
            if(_flowers.Count == 12) replantPot.gameObject.SetActive(false);
            
            TableController.Instance.PlaceFlower(index);

            ShelfFertilizerItem.OnFertilizer += Fertilize;
            CInputManager.Instance.SetNavigation(false);
        }

        private void Fertilize(Environment.FertilizerType type)
        {
            CInputManager.Instance.SetNavigation(true);
            
            // TODO Asserts it is the Last index
            _flowers[^1].Replant(type);
            ShelfFertilizerItem.OnFertilizer -= Fertilize;
            TableController.Instance.PlaceFlower(_flowers.Count - 1);
        }
        
        public FlowerInstance GetFlower(int index)
        {
            Debug.Assert(index < _flowers.Count, $"Error, index of searched flower is to high: {index}");
            return _flowers[index];
        }
    }
}