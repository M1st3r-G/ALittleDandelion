using System;
using System.Linq;
using Data;
using UnityEngine;
using Environment = Data.Environment;

namespace Managers
{
    public class FlowerLibrary : MonoBehaviour
    {
        [SerializeField] private FlowerMeshesAsset allMeshes;
        [SerializeField] private MaterialWithTag[] allDirts;
        
        public static FlowerLibrary Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
        }

        private void OnDestroy()
        {
            if (Instance == this) Instance = null;
        }

        public Mesh GetMesh(FlowerData.FlowerType type, FlowerInstance.GrowthState state)
            => allMeshes.GetMesh(type, state);

        public Material GetDirtMaterial(Environment.SoilType type)
            => allDirts.FirstOrDefault(mWt => mWt.type == type).mat;

        [Serializable]
        private struct MaterialWithTag
        {
            public Environment.SoilType type;
            public Material mat;
        }
    }
}