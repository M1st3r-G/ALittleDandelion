using System;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(menuName = "Data/FlowerMeshesAsset")]
    public class FlowerMeshesAsset : ScriptableObject
    {
        [SerializeField] private FlowerTypedList[] lists;
        
        public Mesh GetMesh(FlowerData.FlowerType type, FlowerInstance.GrowthState state)
        {
            foreach (FlowerTypedList ftl in lists)
            {
                if (ftl.type == type) return ftl.meshes[(int)state];
            }

            return null;
        }
        
        [Serializable]
        private struct FlowerTypedList
        {
            public FlowerData.FlowerType type;
            public Mesh[] meshes;
        }
    }
}