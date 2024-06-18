using Controller.Book;
using Data;
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
            _seedlings.CustomSetActive(false);

            if (_current is not null)
            {
                _current.SetDisplayed(false);
                FlowerInstanceLibrary.Instance.ReturnPot(_current);
            }
            _current = FlowerInstanceLibrary.Instance.BorrowPot(index);
            AdjustCurrentTransform();
            _current.SetDisplayed(true);
            CheckForRemoval();
        }

        private void CheckForRemoval()
        {
            if (!_current.IsDead && !_current.IsFullyGrown) return;
            PlantRemoval.Instance.WaitForRemoval(_current.IsDead, _current.GetRatingOfFlower(out _), RemoveCurrent);
        }
        
        // Replaces the PlaceFlower for Replanting
        public void CenterPot(PotController pot)
        {
            _current = pot;
            AdjustCurrentTransform();
        }

        private void RemoveCurrent()
        {
            //Wenn Flower, Add Knowledge to Book
            if (_current.IsFullyGrown)
            {
                int stars = _current.GetRatingOfFlower(out FlowerData.FlowerType type);
                BookController.Instance.Unlock(type, stars);
            }
            
            _current.Empty();
            FlowerInstanceLibrary.Instance.ReturnPot(_current);
            _current = null;
        }

        private void AdjustCurrentTransform()
        {
            _current.transform.parent = center;
            _current.transform.localPosition = Vector3.zero;
        }
        
        public void PlaceSeeds()
        {
            Debug.Log("Showing Seeds");
            if (_current is not null)
            {
                _current.SetDisplayed(false);
                FlowerInstanceLibrary.Instance.ReturnPot(_current);
                _current = null;
            }
            _seedlings.CustomSetActive(true);
        }
    }
}