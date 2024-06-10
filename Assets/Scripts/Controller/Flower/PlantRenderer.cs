using Data;
using Managers;
using TMPro;
using UnityEngine;
using Environment = Data.Environment;

namespace Controller
{
    public class PlantRenderer : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI debugText;

        [SerializeField] private GameObject defaultFlower;
        [SerializeField] private GameObject defaultDirt;
        
        private FlowerRendererComponent _flower;
        private MeshRenderer _ground;

        private void Awake()
        {
            GameObject tmp = Instantiate(defaultFlower, transform);
            tmp.SetActive(false);
            _flower = tmp.GetComponent<FlowerRendererComponent>();
            
            tmp = Instantiate(defaultDirt,  transform);
            _ground = tmp.GetComponent<MeshRenderer>();
            tmp.SetActive(false);
        }

        public void RefreshVisuals(FlowerInstance flower, Environment env)
        {
            RenderEnvironment(env);
            RenderFlower(flower);
        }

        private void RenderFlower(FlowerInstance flower)
        {
            if (flower is null)
            { 
                _flower.gameObject.SetActive(false);
                return;
            }
            
            _flower.gameObject.SetActive(true);
            debugText.text = flower.ToString();

            _flower.RenderState(flower);
        }

        private void RenderEnvironment(Environment env)
        {
            debugText.text = $"Empty pot: Environment: {env.soil}, {env.lichtkeimer}"; // If no flower, not overwritten
            if (env.soil == Environment.SoilType.None)
            {
                _ground.gameObject.SetActive(false); // Active if not none
            }
            else
            {
                _ground.gameObject.SetActive(env.soil != Environment.SoilType.None); // Active if not none
                _ground.material = FlowerLibrary.Instance.GetDirtMaterial(env.soil);
            }
            
            // if(!env.lichtkeimer) Show hole 
        }
        
        public void DebugClearRender()
        {
            debugText.text = "";
        }
    }
}