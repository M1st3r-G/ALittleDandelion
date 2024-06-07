using System;
using System.Collections.Generic;
using Clickable.Shelf;
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
        private List<Tuple<FlowerInstance, Environment>> _flowers;
        
        public static FlowerDisplay Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
            _flowers = new List<Tuple<FlowerInstance, Environment>>();
        }
        
        private void OnDestroy()
        {
            if (Instance == this) Instance = null;
        }

        public void AddFlower(FlowerInstance f, Environment e)
        {
            _flowers.Add(new Tuple<FlowerInstance, Environment>(f, e));
            potsUIDisplay.SetActive(_flowers.ToArray());
            if (_flowers.Count == 12) replantPot.gameObject.SetActive(false);
        }

        public Tuple<FlowerInstance, Environment> GetFlowerAndEnv(int index)
        {
            Debug.Assert(index < _flowers.Count, $"Error, index of searched flower is to high: {index}");
            return _flowers[index];
        }
    }
}