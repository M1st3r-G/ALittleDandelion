using System;
using System.Linq;
using Data;
using Managers;
using UnityEngine;

namespace Controller.Book
{
    public class BookController : MonoBehaviour
    {
        //ComponentReferences
        [SerializeField] private Vector2 inAndOutOfFrame = new(4.25f, 12f);
        [SerializeField] private PageController pageController;
        [SerializeField] private PageWithUnlockData[] pages;
        
        // Params
        private const float DefaultZ = 8;
        
        //Temps
        private int _currentPage;

        public static BookController Instance { get; private set; }

        private bool IsShown => transform.localPosition.x < 8f;
        
        private void Awake()
        {
            Instance = this;
            
            int[] tmp = SaveGameManager.Instance.GetBookData();
            
            for (int i = 0; i < pages.Length; i++)
            {
                pages[i].unlockValue = tmp[i];
            }

            DisplayBook(false);
        }

        private void OnDestroy()
        {
            if (Instance == this) Instance = null;
        }

        public void ToggleBook()
        {
            Debug.Log($"Toggle book to {!IsShown}");
            DisplayBook(!IsShown);
        }
        
        private void DisplayBook(bool show)
        {
            transform.localPosition = new Vector3(show ? inAndOutOfFrame.x : inAndOutOfFrame.y, 0, DefaultZ);
            
            if (!show) return;
            PageWithUnlockData current = pages[_currentPage];
            pageController.ShowPage(current.page, current.unlockValue);
        }

        public int[] GetSaveData() => pages.Select(p => p.unlockValue).ToArray();
        
        [Serializable]
        public struct PageWithUnlockData
        {
            public PageData page;
            // Set based on Save
            [HideInInspector] public int unlockValue;
        }
    }
}