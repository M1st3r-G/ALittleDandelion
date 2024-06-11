using Data;
using Managers;
using UnityEngine;

namespace Controller
{
    [RequireComponent(typeof(MeshFilter))]
    [RequireComponent(typeof(MeshRenderer))]
    public class FlowerRendererComponent : MonoBehaviour
    {
        private MeshFilter _mesh;

        private void Awake()
        {
            _mesh = GetComponent<MeshFilter>();
        }

        public void RenderState(FlowerInstance flower)
        {
            _mesh.mesh = FlowerArtLibrary.Instance.GetMesh(flower.Type, flower.State);
        }
    }
}