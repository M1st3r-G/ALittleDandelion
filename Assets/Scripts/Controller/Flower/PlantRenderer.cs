using Data;
using Managers;
using TMPro;
using UnityEngine;
using Environment = Data.Environment;

namespace Controller
{
    public class PlantRenderer : MonoBehaviour
    {
        [Header("DirtStuff")]
        [SerializeField] private Mesh dirt;
        [SerializeField] private float dirtHeight;
        [SerializeField] private Mesh dirtWithHole;
        [SerializeField] private float dirtWithHoleHeight;

        [Header("Label")] 
        [SerializeField] private GameObject labelObject;
        [SerializeField] private TextMeshProUGUI textRender;
        
        [Header("RenderComponents")]
        [SerializeField] private FlowerRendererComponent flower;
        [SerializeField] private MeshRenderer groundRenderer;
        [SerializeField] private MeshFilter groundMesh;
        
        public void RefreshVisuals(FlowerInstance pFlower, Environment env)
        {
            RenderEnvironment(env);
            RenderFlower(pFlower);
            RenderLabel(pFlower, env);
        }

        private void RenderFlower(FlowerInstance pFlower)
        {
            if (pFlower is null)
            { 
                flower.gameObject.SetActive(false);
                return;
            }
            
            flower.gameObject.SetActive(true);
            flower.RenderState(pFlower);
        }

        private void RenderEnvironment(Environment env)
        {
            if (env.soil == Environment.SoilType.None)
            {
                groundRenderer.gameObject.SetActive(false); // Active if not none
            }
            else
            {
                groundRenderer.gameObject.SetActive(env.soil != Environment.SoilType.None); // Active if not none
                groundRenderer.material = FlowerLookUpLibrary.Instance.GetDirtMaterial(env.soil);
                
                groundMesh.mesh = env.lichtkeimer ? dirt : dirtWithHole;
                groundRenderer.transform.localPosition = new Vector3(0, env.lichtkeimer ? dirtHeight : dirtWithHoleHeight, 0);
            }
        }

        private void RenderLabel(FlowerInstance pFlower, Environment pEnv)
        {
            if (pFlower is not null)
            {
                labelObject.SetActive(true);
                string tmp = pEnv.lichtkeimer ? "LK" : "DK";
                textRender.text = $"Ein(e) {pFlower} in einem {pEnv.soil} ({tmp})";
                return;
            }

            if (pEnv.soil == Environment.SoilType.None) labelObject.SetActive(false);
            else
            {
                labelObject.SetActive(true);
                textRender.text = $"Empty pot: Environment: {pEnv.soil}, {pEnv.lichtkeimer}";
            }
        }
    }
}