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
                    stalkMesh.gameObject.transform.localPosition = finalMeshes[0].offset;

                    blossomMesh.mesh = finalMeshes[1].mesh;
                    blossomRenderer.material = finalMeshes[1].material;
                    blossomMesh.gameObject.transform.localPosition = finalMeshes[1].offset;
                    return;
                }
                case FlowerInstance.GrowthState.Dead:
                    FlowerMeshesAsset.MeshWithMaterial deadMesh = FlowerLookUpLibrary.Instance.GetDeadMesh();
                    stalkMesh.mesh = deadMesh.mesh;
                    stalkRenderer.material = deadMesh.material;
                    stalkMesh.gameObject.transform.localPosition = deadMesh.offset;
                    blossomMesh.mesh = null;
                    return;
                case FlowerInstance.GrowthState.Seed:
                case FlowerInstance.GrowthState.Sprout:
                case FlowerInstance.GrowthState.Bloom:
                default:
                    FlowerMeshesAsset.MeshWithMaterial mwM = FlowerLookUpLibrary.Instance.GetMesh(flower.Type, flower.State);
                    stalkMesh.mesh = mwM.mesh;
                    stalkRenderer.material = mwM.material;
                    stalkMesh.gameObject.transform.localPosition = mwM.offset;
                    blossomMesh.mesh = null;
                    return;
            }
        }
    }
}