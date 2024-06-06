using Data;
using UnityEngine;

namespace Managers
{
    public class Storage : MonoBehaviour
    {
        [SerializeField] private FlowerData debugFlower;
        [SerializeField] private Environment debugEnvironment;
        private FlowerInstance[] _allPots;
        private FlowerInstance[] _allSeeds;
        
        public static Storage Instance { get; private set; }
        
        private void Awake()
        {
            Instance = this;
            _allPots = new FlowerInstance[12];
            _allSeeds = new FlowerInstance[6];

            _allSeeds[1] = new FlowerInstance(debugFlower, debugEnvironment);
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