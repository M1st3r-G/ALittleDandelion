using System;
using System.Collections.Generic;
using Controller;
using Data;
using UnityEngine;
using UnityEngine.InputSystem;
using Environment = Data.Environment;

namespace Managers
{
    public class SaveGameManager : MonoBehaviour
    {
        [SerializeField] private InputAction debugAction;
        [SerializeField] private SeedBoxesController seeds;

        private SaveFileObject _loadedState;
        
        public static SaveGameManager Instance { get; private set; }
        public const string PrefSaveKey = "SavedGames";
        
        private void Awake()
        {
            Instance = this;

            string tmp = PlayerPrefs.GetString(PrefSaveKey, "");
            _loadedState = tmp == "" ? null : JsonUtility.FromJson<SaveFileObject>(tmp);
            
            Debug.LogWarning(tmp);
            debugAction.Enable();
            debugAction.performed += SaveGame;
        }

        #region RetrieveSavedData

        public void GetSeedBoxData(out FlowerInstance[] flowers, out Environment[] envs)
        {
            ReadSavedLists(_loadedState.seedMap, 6, _loadedState.seedBoxFlowers, _loadedState.seedBoxEnvironments, out flowers, out envs);
        }

        public void GetInstanceData(out FlowerInstance[] flowers, out Environment[] envs)
        {
            ReadSavedLists(_loadedState.instanceMap, 12, _loadedState.flowerInstances, _loadedState.environmentInstances, out flowers, out envs);
        }

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
            SaveGame(new InputAction.CallbackContext());
        }

        private void SaveGame(InputAction.CallbackContext _)
        {
            string tmp = GenerateSaveFile();
            Debug.LogWarning(tmp);
            PlayerPrefs.SetString(PrefSaveKey, tmp);
        }

        private string GenerateSaveFile() => JsonUtility.ToJson(new SaveFileObject(seeds));

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
            
            public SaveFileObject(SeedBoxesController seeds)
            {
                seeds.GetSaveContent(out seedBoxFlowers, out seedBoxEnvironments, out seedMap);
                FlowerInstanceLibrary.Instance.GetSaveContent(out flowerInstances, out environmentInstances, out instanceMap);
                dayCount = TimeManager.Instance.Days;
            }
        }
    
        #endregion
    }
}