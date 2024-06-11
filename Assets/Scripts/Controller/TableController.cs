using Managers;
using UnityEngine;

namespace Controller
{
    public class TableController : MonoBehaviour
    {
        [SerializeField] private Transform center;
        private SeedBoxesController _seedlings;

        private PotController _current;
        
        public static TableController Instance { get; private set; }
        
        private void Awake()
        {
            Instance = this;
            _seedlings = GetComponentInChildren<SeedBoxesController>(true);
        }

        public void PlaceFlower(int index)
        {
            Debug.Log($"Flower with index: {index}");
            _seedlings.gameObject.SetActive(false);

            if (_current is not null) FlowerInstanceLibrary.Instance.ReturnPot(_current);
            _current = FlowerInstanceLibrary.Instance.BorrowPot(index);
            _current.transform.position = center.position;
        }

        // Replaces the PlaceFlower for Replanting
        public void CenterPot(PotController pot)
        {
            //Should be Redundant _seedlings.gameObject.SetActive(false);
            _current = pot;
            _current.transform.position = center.position;
        }
        
        public void PlaceSeeds()
        {
            Debug.Log("Showing Seeds");
            if (_current is not null)
            {
                FlowerInstanceLibrary.Instance.ReturnPot(_current);
                _current = null;
            }
            _seedlings.gameObject.SetActive(true);
        }
    }
}