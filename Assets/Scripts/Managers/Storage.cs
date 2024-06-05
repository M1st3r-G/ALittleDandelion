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
            _allPots = new FlowerInstance[12];
            _allSeeds = new FlowerInstance[6];
            Instance = this;
        }

        private void OnDestroy()
        {
            if (Instance == this) Instance = this;
        }

        public FlowerInstance[] GetSeeds() => _allSeeds;
        public void StoreSeeds(FlowerInstance[] seeds) => _allSeeds = seeds;

        public void SetPlant(int index, FlowerInstance flower) => _allPots[index] = flower;
        public FlowerInstance GetFlower(int index) => _allPots[index];
    }
}