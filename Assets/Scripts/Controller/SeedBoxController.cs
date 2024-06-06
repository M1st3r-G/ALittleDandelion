using Clickable;
using Data;
using Managers;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using Environment = Data.Environment;

namespace Controller
{
    public class SeedBoxController : ClickableBase
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

        private new void Awake()
        {
            base.Awake();
            _parent = GetComponentInParent<SeedBoxesController>();
            _meshRenderer = GetComponent<MeshRenderer>();
            _defaultMaterial = _meshRenderer.material;
        }

        #region SelectionHandling

        public override void OnPointerClick(PointerEventData eventData)
        {
            _parent.BoxWasClicked(this);
        }

        public void Select()
        {
            _meshRenderer.material = selectedMaterial;
            if (_flower is null)
            {
                debugText.text = $"Empty pot: Environment: {_tmp.soil}, {_tmp.lichtkeimer}";
            }
            else
            {
                _flower.OnChange += RefreshVisuals;
            }
        }

        public void Deselect()
        {
            debugText.text = "";
            _meshRenderer.material = _defaultMaterial;
            if (_flower is not null) _flower.OnChange -= RefreshVisuals;
        }

        #endregion

        #region CareAndSet

        public void AddFlower(FlowerData flower)
        {
            if (!EnvironmentIsSet) Debug.LogWarning("Environment Is Not Set");
            else
            {
                Debug.Log("SetFlower");
                _flower = new FlowerInstance(flower, _tmp);
                _flower.OnChange += RefreshVisuals;
                RefreshVisuals();
            }
        }
        
        public void AddSoil(Environment.SoilType type)
        {
            Debug.Assert(!EnvironmentIsSet, $"Fehler, Soil wurde im Environment Überschrieben!{name}: {_flower}");
            Debug.Log("Added Soil to Pot");
            _tmp.soil = type;
            RefreshVisuals();
        }

        public void ChangeLightType()
        {
            _tmp.lichtkeimer = !_tmp.lichtkeimer;
            RefreshVisuals();
        }

        public void WaterPlant()
        {
            Debug.LogWarning("Plant is Watered");
            _flower.Water();
        }

        public void Replant()
        {
            _flower.OnChange -= RefreshVisuals;
            TableController.Instance.ReplantFlower(_flower);
            _flower = null;
            _tmp = new Environment();
        }
        
        private void RefreshVisuals()
        {
            debugText.text = _flower is not null ? _flower.ToString() : $"Empty pot: Environment: {_tmp.soil}, {_tmp.lichtkeimer}";
        }

        public bool EnvironmentIsSet => _tmp.soil != Environment.SoilType.None;
        public bool IsReplantable => _flower is null ? false : _flower.IsReplantable;

        #endregion
    }
}