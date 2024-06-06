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
        [SerializeField] private PotButtons potsUIDisplay;
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
            _flowers.Add(f);
            potsUIDisplay.SetActive(_flowers.ToArray());
            if (_flowers.Count == 12) replantPot.gameObject.SetActive(false);
        }

        public FlowerInstance GetFlower(int index)
        {
            Debug.Assert(index < _flowers.Count, $"Error, index of searched flower is to high: {index}");
            return _flowers[index];
        }
    }
}