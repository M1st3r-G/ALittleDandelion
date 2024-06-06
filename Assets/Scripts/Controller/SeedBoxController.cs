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

        [SerializeField] private TextMeshProUGUI debugText;
        [SerializeField] private Material selectedMaterial;
        private SeedBoxesController _parent;
        private Material _defaultMaterial;
        private MeshRenderer _meshRenderer;
        
        public FlowerInstance Flower
        {
            set
            {
                _flower = value;
                Display();
            }
            get => _flower;
        }
        public bool IsEditable => _flower is null;
        private FlowerInstance _flower;
        private Environment _tmp;

        #endregion

        private void Awake()
        {
            _parent = GetComponentInParent<SeedBoxesController>();
            _meshRenderer = GetComponent<MeshRenderer>();
            _defaultMaterial = _meshRenderer.material;
        }

        private void Display()
        {
            if (IsEditable) Debug.LogWarning("This Flower is editable");
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            _parent.BoxWasClicked(this);
            debugText.text = _flower is not null ? _flower.ToString() : "";
            _meshRenderer.material = selectedMaterial;
        }

        public void Deselect()
        {
            _meshRenderer.material = _defaultMaterial;
        }

        public void AddFlower(FlowerData flower)
        {
            if (!EnvironmentIsSet())
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
        }

        private bool EnvironmentIsSet() => _tmp.soil != Environment.SoilType.None;
    }
}