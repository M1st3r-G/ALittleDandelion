using System;
using System.Linq;
using Data;
using UnityEngine;
using Environment = Data.Environment;

namespace Managers
{
    public class FlowerLookUpLibrary : MonoBehaviour
    {
        [SerializeField] private FlowerMeshesAsset allMeshes;
        [SerializeField] private MaterialWithTag[] allDirts;
        [SerializeField] private FlowerData[] allFlowerData;

        public int NumberOfFlowers => allFlowerData.Length;
        public static FlowerLookUpLibrary Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
        }

        private void OnDestroy()
        {
            if (Instance == this) Instance = null;
        }

        public FlowerMeshesAsset.MeshWithMaterial GetMesh(FlowerData.FlowerType type, FlowerInstance.GrowthState state)
            => allMeshes.GetMesh(type, state);

        public FlowerMeshesAsset.MeshWithMaterial[] GetFinalMesh(FlowerData.FlowerType type)
            => allMeshes.GetFinalMeshes(type);

        public FlowerMeshesAsset.MeshWithMaterial GetDeadMesh()
            => allMeshes.GetDeadMesh();
        
        public Material GetDirtMaterial(Environment.SoilType type)
            => allDirts.FirstOrDefault(mWt => mWt.type == type).mat;

        public FlowerData GetFlowerData(FlowerData.FlowerType type)
            => allFlowerData.FirstOrDefault(f => f.FlowerName == type);

        
        [Serializable]
        private struct MaterialWithTag
        {
            public Environment.SoilType type;
            public Material mat;
        }
    }
}