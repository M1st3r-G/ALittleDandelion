using System.Linq;
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

        private void Start() => FetchFlowers();
        
        private void FetchFlowers()
        {
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
        }

        public void BoxWasClicked(SeedBoxController box)
        {
            Debug.Log("SeedBox was Clicked");
            if (_currentSelection is not null) _currentSelection.Deselect();
            _currentSelection = box;
        }
    }
}