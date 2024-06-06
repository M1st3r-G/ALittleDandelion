using Data;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using Environment = Data.Environment;

namespace Controller
{
    public class SeedBoxController : MonoBehaviour, IPointerClickHandler
    {
        #region Fields
        // Component References
        [SerializeField] private TextMeshProUGUI debugText;
        [SerializeField] private Material selectedMaterial;
        private SeedBoxesController _parent;
        private Material _defaultMaterial;
        private MeshRenderer _meshRenderer;
        
        // Temps
        private FlowerInstance _flower;
        private Environment _tmp;
        
        // Public
        public bool IsEditable => _flower is null;
        
        #endregion

        private void Awake()
        {
            _parent = GetComponentInParent<SeedBoxesController>();
            _meshRenderer = GetComponent<MeshRenderer>();
            _defaultMaterial = _meshRenderer.material;
        }

        #region SelectionHandling

        public void OnPointerClick(PointerEventData eventData)
        {
            _parent.BoxWasClicked(this);
        }

        public void Select()
        {
            debugText.text = _flower is not null ? _flower.ToString() : $"Empty pot: Environment: {_tmp.soil}, {_tmp.lichtkeimer}";
            _meshRenderer.material = selectedMaterial;
        }

        public void Deselect()
        {
            debugText.text = "";
            _meshRenderer.material = _defaultMaterial;
        }

        #endregion

        #region CareAndSet

        public void AddFlower(FlowerData flower)
        {
            if (_tmp.soil == Environment.SoilType.None)
                Debug.LogWarning("Environment Is Not Set");
            else
            {
                Debug.Log("SetFlower");
                _flower = new FlowerInstance(flower, _tmp);
                debugText.text = _flower.ToString();
            }
        }

        public void AddSoil(Environment.SoilType type)
        {
            Debug.Assert(_tmp.soil == Environment.SoilType.None, $"Fehler, Soil wurde im Environment Überschrieben!{name}: {_flower}");
            Debug.Log("Added Soil to Pot");
            _tmp.soil = type;
            debugText.text = $"Empty pot: Environment: {_tmp.soil}, {_tmp.lichtkeimer}";
        }

        public void WaterPlant()
        {
            Debug.LogWarning("Plant is Watered");
            _flower.Water();
            debugText.text = _flower.ToString();
        }

        #endregion
    }
}