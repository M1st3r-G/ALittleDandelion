using System;
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
        public static SaveGameManager Instance { get; private set; }
        
        private void Awake()
        {
            Instance = this;
            debugAction.Enable();
            debugAction.performed += Test;
        }

        private void Test(InputAction.CallbackContext obj)
        {
            Debug.Log(GenerateSaveFile());
        }

        private string GenerateSaveFile()
        {
            return JsonUtility.ToJson(new SaveFileObject(seeds), true);
        }

        [Serializable]
        private class SaveFileObject
        {
            public FlowerInstance.FlowerSerialization[] seedBoxFlowers;
            public Environment[] seedBoxEnvironments;
            public FlowerInstance.FlowerSerialization[] flowerInstances;
            public Environment[] environmentInstances;

            public SaveFileObject(SeedBoxesController seeds)
            {
                seeds.GetSaveContent(out seedBoxFlowers, out seedBoxEnvironments);
                FlowerInstanceLibrary.Instance.GetSaveContent(out flowerInstances, out environmentInstances);
            }
        }
    }
}