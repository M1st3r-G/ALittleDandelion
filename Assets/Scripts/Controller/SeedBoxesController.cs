using System.Linq;
using Data;
using Managers;
using UnityEngine;

namespace Controller
{
    public class SeedBoxesController : MonoBehaviour
    {
        private SeedBoxController[] _boxes;

        private void Awake()
        {
            _boxes = GetComponentsInChildren<SeedBoxController>();
        }

        private void Start() => Init();
        
        private void Init()
        {
            Debug.LogError("Looking for Seeds");
            FlowerInstance[] flowers = Storage.Instance.GetSeeds();
            for (int i = 0; i < flowers.Length; i++)
            {
                _boxes[i].Flower = flowers[i];
            }
        }

        private void DeInit()
        {
            Storage.Instance.StoreSeeds(_boxes.Select(b => b.Flower).ToArray());
        }
    }
}