using Data;
using UnityEngine;

namespace Managers
{
    public class Storage : MonoBehaviour
    {
        private FlowerInstance[] _allPots;
        private FlowerInstance[] _allSeeds;
        
        public static Storage Instance { get; private set; }
        
        private void Awake()
        {
            Instance = this;
            Debug.LogError("Created Storage");
            _allPots = new FlowerInstance[12];
            _allSeeds = new FlowerInstance[6];
        }

        private void OnDestroy()
        {
            if (Instance == this) Instance = this;
        }

        public FlowerInstance[] GetSeeds()
        {
            return _allSeeds;
        }

        public void StoreSeeds(FlowerInstance[] seeds) => _allSeeds = seeds;

        public void SetPlant(int index, FlowerInstance flower) => _allPots[index] = flower;
        public FlowerInstance GetFlower(int index) => _allPots[index];
    }
}