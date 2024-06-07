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
        
        public void RenderState(FlowerInstance flower)
        {
            _mesh.mesh = FlowerLibrary.Instance.GetMesh(flower.Type, flower.State);
        }
    }
}