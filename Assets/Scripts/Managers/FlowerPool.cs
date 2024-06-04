using Data;
using UnityEngine;

namespace Managers
{
    public class FlowerPool : MonoBehaviour
    {
        private FlowerInstance[] _flowers;
        public static FlowerPool Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
            _flowers = new FlowerInstance[12];
        }
        
        public FlowerInstance GetFlower(int index) => _flowers[index];
    }
}