﻿using System;
using System.Collections.Generic;
using Controller;
using Controller.Book;
using Data;
using UnityEngine;
using Environment = Data.Environment;

namespace Managers
{
    public class SaveGameManager : MonoBehaviour
    {
        [Header("References to Savable Objects")]
        [SerializeField] private SeedBoxesController seeds;
        [SerializeField] private FlowerInstanceLibrary library;
        [SerializeField] private TimeManager timeManager;
        [SerializeField] private BookController book;
        
        //Temps
        private SaveFileObject _loadedState;
        
        //Publics
        public static SaveGameManager Instance { get; private set; }
        public const string PrefSaveKey = "SavedGames";
        
        private void Awake()
        {
            Instance = this;

            string tmp = PlayerPrefs.GetString(PrefSaveKey, "");
            _loadedState = tmp == "" ? null : JsonUtility.FromJson<SaveFileObject>(tmp);
            
            Debug.LogWarning(tmp);
        }

        private void Start()
        {
            if (_loadedState is null) TutorialManager.Instance.StartTutorial();
        }

        #region RetrieveSavedData

        public void GetSeedBoxData(out FlowerInstance[] flowers, out Environment[] envs)
        {
            if (_loadedState is null)
            {
                flowers = new FlowerInstance[6];
                envs = new Environment[6];
            }
            else
            {
                ReadSavedLists(_loadedState.seedMap, 6, _loadedState.seedBoxFlowers, _loadedState.seedBoxEnvironments,
                    out flowers, out envs);
            }
        }

        public void GetInstanceData(out FlowerInstance[] flowers, out Environment[] envs)
        {
            if (_loadedState is null)
            {
                flowers = new FlowerInstance[12];
                envs = new Environment[12];
            }
            else
            {
                ReadSavedLists(_loadedState.instanceMap, 12, _loadedState.flowerInstances,
                    _loadedState.environmentInstances, out flowers, out envs);
            }
        }

        public int GetTimeData() => _loadedState is null ? 1 : _loadedState.dayCount;

        public int[] GetBookData() => _loadedState is null ? new int[FlowerLookUpLibrary.Instance.NumberOfFlowers] : _loadedState.bookUnlockValues;

        private static void ReadSavedLists(int map, int numberOfItems, FlowerInstance.FlowerSerialization[] readFlowers, Environment[] readEnv, out FlowerInstance[] rFlowers, out Environment[] rEnvironment)
        {
            var tmpFlower = new List<FlowerInstance>();
            var tmpEnvironments = new List<Environment>();

            int c = 0;
            
            for (int i = 0; i < numberOfItems; i++)
            {
                bool plant = map % 2 == 1;
                if (plant)
                {
                    tmpFlower.Add(new FlowerInstance(readFlowers[c]));
                    tmpEnvironments.Add(readEnv[c]);
                    c++;
                }
                else
                {
                    tmpFlower.Add(null); 
                    tmpEnvironments.Add(new Environment());
                }
                
                map >>= 1;
            }
            
            Debug.Assert(tmpEnvironments.Count == numberOfItems, "Fehler beim Parsen");
            Debug.Assert(tmpFlower.Count == numberOfItems, "Fehler beim Parsen");

            rFlowers = tmpFlower.ToArray();
            rEnvironment = tmpEnvironments.ToArray();
        }
        
        #endregion
        
        #region Saving

        private void OnDestroy()
        {
            if (Instance == this) Instance = null;
            SaveGame();
        }

        private void SaveGame()
        {
            string tmp = GenerateSaveFile();
            Debug.LogWarning(tmp);
            PlayerPrefs.SetString(PrefSaveKey, tmp);
        }

        private string GenerateSaveFile() => JsonUtility.ToJson(new SaveFileObject(seeds, library, timeManager, book));

        [Serializable]
        private class SaveFileObject
        {
            public FlowerInstance.FlowerSerialization[] seedBoxFlowers;
            public Environment[] seedBoxEnvironments;
            public int seedMap;
            
            public FlowerInstance.FlowerSerialization[] flowerInstances;
            public Environment[] environmentInstances;
            public int instanceMap;
            
            public int dayCount;

            public int[] bookUnlockValues;
            
            public SaveFileObject(SeedBoxesController seeds, FlowerInstanceLibrary library, TimeManager time, BookController book)
            {
                seeds.GetSaveContent(out seedBoxFlowers, out seedBoxEnvironments, out seedMap);
                library.GetSaveContent(out flowerInstances, out environmentInstances, out instanceMap);
                dayCount = time.Days;
                bookUnlockValues = book.GetSaveData();
            }
        }
    
        #endregion
    }
}