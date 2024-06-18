using Data;
using Managers;
using UnityEngine;

namespace Controller
{
    public class FlowerRendererComponent : MonoBehaviour
    {
        [SerializeField] private MeshFilter stalkMesh;
        [SerializeField] private MeshFilter blossomMesh;
        [SerializeField] private MeshRenderer stalkRenderer;
        [SerializeField] private MeshRenderer blossomRenderer;

        public void RenderState(FlowerInstance flower)
        {
            switch (flower.State)
            {
                case FlowerInstance.GrowthState.Flower:
                {
                    FlowerMeshesAsset.MeshWithMaterial[] finalMeshes =
                        FlowerLookUpLibrary.Instance.GetFinalMesh(flower.Type);

                    stalkMesh.mesh = finalMeshes[0].mesh;
                    stalkRenderer.material = finalMeshes[0].material;

                    blossomMesh.mesh = finalMeshes[1].mesh;
                    blossomRenderer.material = finalMeshes[1].material;
                    //Fix Stalk Position
                    //Fix Blossom Position
                    return;
                }
                case FlowerInstance.GrowthState.Dead:
                    Debug.LogError("The Dead Mesh ist missing");
                    return;
                case FlowerInstance.GrowthState.Seed:
                case FlowerInstance.GrowthState.Sprout:
                case FlowerInstance.GrowthState.Bloom:
                default:
                    FlowerMeshesAsset.MeshWithMaterial mwM = FlowerLookUpLibrary.Instance.GetMesh(flower.Type, flower.State);
                    stalkMesh.mesh = mwM.mesh;
                    stalkRenderer.material = mwM.material;
                    blossomMesh.mesh = null;
                    return;
            }
        }
    }
}