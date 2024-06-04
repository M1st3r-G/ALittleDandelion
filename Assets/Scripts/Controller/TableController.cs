using UnityEngine;

namespace Controller
{
    public class TableController : MonoBehaviour
    {
        [SerializeField] private Transform potCenter;

        public static TableController Instance { get; private set; }
        
        private void Awake()
        {
            Instance = this;
        }

        public void DisplayFlower(int index)
        {
            Debug.Log($"Flower with index: {index}");
        }
    }
}