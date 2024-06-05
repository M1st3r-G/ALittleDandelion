using UnityEngine;

namespace Controller
{
    public class TableController : MonoBehaviour
    {
        [SerializeField] private Transform potCenter;
        [SerializeField] private GameObject pot;
        [SerializeField] private GameObject seedlings;

        public static TableController Instance { get; private set; }
        
        private void Awake()
        {
            Instance = this;
        }

        public void PlaceFlower(int index)
        {
            Debug.Log($"Flower with index: {index}");
            seedlings.gameObject.SetActive(false);
            pot.gameObject.SetActive(true);
            //pot.Render(index)
        }

        public void PlaceSeeds()
        {
            Debug.Log("Showing Seeds");
            pot.gameObject.SetActive(false);
            seedlings.gameObject.SetActive(true);
        }
    }
}