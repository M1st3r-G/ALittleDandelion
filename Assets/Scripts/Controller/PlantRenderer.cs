using Data;
using TMPro;
using UnityEngine;

namespace Controller
{
    public class PlantRenderer : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI debugText;
        
        public void RefreshVisuals(FlowerInstance flower, Environment env)
        {
            debugText.text = flower is null ? $"Empty pot: Environment: {env.soil}, {env.lichtkeimer}" : flower.ToString();
        }
        
        public void DebugClearRender()
        {
            debugText.text = "";
        }
    }
}