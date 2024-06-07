using Data;
using Managers;
using UnityEngine;

namespace Controller
{
    public class TableController : MonoBehaviour
    {
       private PotController _pot;
       private SeedBoxesController _seedlings;

        public static TableController Instance { get; private set; }
        
        private void Awake()
        {
            Instance = this;
            _pot = GetComponentInChildren<PotController>(true);
            _seedlings = GetComponentInChildren<SeedBoxesController>(true);
        }

        public void ReplantFlower(FlowerInstance flower, Environment e)
        {
            //Visually and Save
            FlowerDisplay.Instance.AddFlower(flower, e);

            _seedlings.gameObject.SetActive(false);
            _pot.Replant(flower, e);
        }

        public void PlaceFlower(int index)
        {
            Debug.Log($"Flower with index: {index}");
            _seedlings.gameObject.SetActive(false);
            _pot.SetActive(index);
        }

        public void PlaceSeeds()
        {
            Debug.Log("Showing Seeds");
            _pot.gameObject.SetActive(false);
            _seedlings.gameObject.SetActive(true);
        }
    }
}