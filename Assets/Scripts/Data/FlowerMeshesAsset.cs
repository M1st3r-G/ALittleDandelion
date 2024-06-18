using System;
using System.Linq;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(menuName = "Data/FlowerMeshesAsset")]
    public class FlowerMeshesAsset : ScriptableObject
    {
        [SerializeField] private TypedFlowerInformation[] lists;
        [SerializeField] private MeshWithMaterial deadMesh;
        public MeshWithMaterial GetDeadMesh() => deadMesh;
        
        public MeshWithMaterial GetMesh(FlowerData.FlowerType type, FlowerInstance.GrowthState state)
        {
            Debug.Assert(state != FlowerInstance.GrowthState.Dead && state != FlowerInstance.GrowthState.Flower,
                "Illegaler Mesh abgefragt");
            
            foreach (TypedFlowerInformation flowerInformation in lists)
            {
                if (flowerInformation.type == type) return flowerInformation.meshes[(int)state];
            }
            
            return default;
        }

        public MeshWithMaterial[] GetFinalMeshes(FlowerData.FlowerType type)
            => (from fI in lists where fI.type == type select fI.final).FirstOrDefault();
        
        [Serializable]
        private struct TypedFlowerInformation
        {
            public string name;
            public FlowerData.FlowerType type;
            public MeshWithMaterial[] meshes;
            public MeshWithMaterial[] final;
        }

        [Serializable]
        public struct MeshWithMaterial
        {
            public Mesh mesh;
            public Material material;
            public Vector3 offset;
        }
    }
}