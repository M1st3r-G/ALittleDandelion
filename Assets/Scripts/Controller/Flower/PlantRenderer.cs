using Data;
using Managers;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using Environment = Data.Environment;

namespace Controller
{
    public class PlantRenderer : MonoBehaviour
    {
        [SerializeField] private Mesh dirt;
        [SerializeField] private float dirtHeight;
        [SerializeField] private Mesh dirtWithHole;
        [SerializeField] private float dirtWithHoleHeight;
        
        private TextMeshProUGUI _debugText;
        
        [SerializeField] private FlowerRendererComponent flower;
        [SerializeField] private MeshRenderer groundRenderer;
        [SerializeField] private MeshFilter groundMesh;
        

        private void Awake()
        {
            _debugText = GameObject.FindGameObjectWithTag("Debug").GetComponent<TextMeshProUGUI>();
        }

        public void RefreshVisuals(FlowerInstance pFlower, Environment env)
        {
            RenderEnvironment(env);
            RenderFlower(pFlower);
        }

        private void RenderFlower(FlowerInstance pFlower)
        {
            if (pFlower is null)
            { 
                flower.gameObject.SetActive(false);
                return;
            }
            
            flower.gameObject.SetActive(true);
            _debugText.text = pFlower.ToString();

            flower.RenderState(pFlower);
        }

        private void RenderEnvironment(Environment env)
        {
            _debugText.text = $"Empty pot: Environment: {env.soil}, {env.lichtkeimer}"; // If no flower, not overwritten
            if (env.soil == Environment.SoilType.None)
            {
                groundRenderer.gameObject.SetActive(false); // Active if not none
            }
            else
            {
                groundRenderer.gameObject.SetActive(env.soil != Environment.SoilType.None); // Active if not none
                groundRenderer.material = FlowerLookUpLibrary.Instance.GetDirtMaterial(env.soil);
                
                groundMesh.mesh = env.lichtkeimer ? dirt : dirtWithHole;
                groundRenderer.transform.position = new Vector3(0, env.lichtkeimer ? dirtHeight : dirtWithHoleHeight, 0);
            }
            
            // if(!env.lichtkeimer) Show hole 
        }
        
        public void DebugClearRender()
        {
            _debugText.text = "";
        }
    }
}