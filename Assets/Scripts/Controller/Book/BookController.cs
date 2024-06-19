﻿using System;
using System.Collections.Generic;
using System.Linq;
using Data;
using Managers;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Controller.Book
{
    public class BookController : MonoBehaviour
    {
        #region Fields

        //ComponentReferences
        [SerializeField] private Vector2 inAndOutOfFrame = new(4.25f, 12f);
        [SerializeField] private PageController pageController;
        [SerializeField] private PageWithUnlockData[] pages;

        [SerializeField] private Button leftButton;
        [SerializeField] private Button rightButton;
        
        // Params
        private Vector3 _defaultPos;
        
        //Temps
        private int _currentPage;

        public static BookController Instance { get; private set; }

        public bool IsShown => transform.localPosition.x < 8f;

        #endregion

        #region SetUp

        private void Awake()
        {
            Instance = this;
            
            int[] tmp = SaveGameManager.Instance.GetBookData();
            
            for (int i = 0; i < pages.Length; i++)
            {
                pages[i].unlockValue = tmp[i];
            }

            _defaultPos = transform.localPosition;
            
            leftButton.gameObject.SetActive(false);
            DisplayBook(false);
        }

        private void OnDestroy()
        {
            if (Instance == this) Instance = null;
        }

        #endregion

        #region UnlockFlagManagement

        public void Unlock(FlowerData.FlowerType flower, int stars)
        {
            Debug.LogWarning($"Adding {stars} stars to the {flower} flower Entry");
            int index = FindPageIndexWithFlowerType(flower);
            int oldUnlockValue = pages[index].unlockValue;

            Debug.Log($"Prev UValue: {oldUnlockValue}");
            
            int newUnlockedHintsFlag = ModifyUnlockedHintsFlag(stars, oldUnlockValue >> 1);
            int newUnlockValue = (newUnlockedHintsFlag << 1) | 1;
            
            Debug.Log($"New UValue: {newUnlockValue}");

            pages[index].unlockValue = newUnlockValue;
        }

        private static int ModifyUnlockedHintsFlag(int stars, int unlockedHintsFlag)
        {
            if (unlockedHintsFlag != 31)
            {
                List<int> set = GetSetOfNeededOptions(unlockedHintsFlag);
                for (int i = 0; i < stars; i++)
                {
                    int rndIdx = Random.Range(0, set.Count);
                    int rnd = set[rndIdx];
                    set.RemoveAt(rndIdx);

                    int key = 1 << rnd;
                    unlockedHintsFlag |= key;
                }
            }

            return unlockedHintsFlag;
        }
        
        private static List<int> GetSetOfNeededOptions(int unlockedHintsFlag)
        {
            List<int> set = new List<int>();

            int tmp = 1;
            for (int i = 0; i < 5; i++)
            {
                if ((unlockedHintsFlag & tmp) == 0) set.Add(i);

                tmp <<= 1;
            }

            Debug.Assert(set.Count != 0, "Liste ist Leer");
            
            return set;
        }

        private int FindPageIndexWithFlowerType(FlowerData.FlowerType type)
        {
            for (var i = 0; i < pages.Length; i++)
            {
                var pWud = pages[i];
                if (pWud.page.Type == type) return i;
            }

            throw new Exception($"There Was no fitting Page for {type}");
        }

        #endregion

        #region DisplayManagement

        public void ToggleBook()
        {
            Debug.Log($"Toggle book to {!IsShown}");
            DisplayBook(!IsShown);
        }

        public void FlipPage(int direction)
        {
            Debug.Assert(IsShown, "Fehler, Seite im Ausgeblendeten zustand geblättert");
            int prev = _currentPage;
            _currentPage = Mathf.Clamp(_currentPage + direction, 0, pages.Length - 1);

            if (prev == _currentPage) return;
            
            PageWithUnlockData current = pages[_currentPage];
            AudioManager.Instance.PlayEffect(AudioManager.AudioEffect.BookPageFlip);
            pageController.ShowPage(current.page, current.unlockValue);

            //ButtonManagement
            if (_currentPage == 0 || _currentPage == pages.Length)
            {
                if (_currentPage == 0) leftButton.gameObject.SetActive(false);
                else rightButton.gameObject.SetActive(false);
            }
            else
            {
                leftButton.gameObject.SetActive(true);
                rightButton.gameObject.SetActive(true);
            }
        }
        
        private void DisplayBook(bool show)
        {
            Vector3 tmp = _defaultPos;
            tmp.x = show ? inAndOutOfFrame.x : inAndOutOfFrame.y;
            transform.localPosition = tmp;
            
            if (!show) return;
            TutorialManager.Instance.SetFlag(TutorialManager.TutorialFlag.BookOpened);
            PageWithUnlockData current = pages[_currentPage];
            pageController.ShowPage(current.page, current.unlockValue);
        }

        #endregion
        
        #region Utils
        
        public int[] GetSaveData() => pages.Select(p => p.unlockValue).ToArray();
        
        [Serializable]
        public struct PageWithUnlockData
        {
            public PageData page;
            // Set based on Save
            [HideInInspector] public int unlockValue;
        }
        
        #endregion
    }
}