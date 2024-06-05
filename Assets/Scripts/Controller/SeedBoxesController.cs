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
            _boxes = new SeedBoxController[6];
        }

        private void OnEnable()
        {
            FlowerInstance[] flowers = Storage.Instance.GetSeeds();
            for (int i = 0; i < flowers.Length; i++)
            {
                _boxes[i].Flower = flowers[i];
            }
        }

        private void OnDisable()
        {
            Storage.Instance.StoreSeeds(_boxes.Select(b => b.Flower).ToArray());
        }
    }
}