using Data;
using UnityEngine;

namespace Managers
{
    public class FlowerLibrary : MonoBehaviour
    {
        [SerializeField] private FlowerMeshesAsset allMeshes;
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
    }
}