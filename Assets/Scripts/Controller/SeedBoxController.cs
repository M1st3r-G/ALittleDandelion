﻿using Clickable;
using Data;
using UnityEngine;
using UnityEngine.EventSystems;
using Environment = Data.Environment;

namespace Controller
{
    [RequireComponent(typeof(PlantRenderer))]
    public class SeedBoxController : ClickableBase
    {
        #region Fields
        
        // Component References
        [SerializeField] private Material selectedMaterial;
        private SeedBoxesController _parent;
        private Material _defaultMaterial;
        private MeshRenderer _meshRenderer;
        private PlantRenderer _pr;
        
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
            _pr = GetComponent<PlantRenderer>();
        }

        #region SelectionHandling

        public override void OnPointerClick(PointerEventData eventData)
        {
            _parent.BoxWasClicked(this);
        }

        public void Select()
        {
            _meshRenderer.material = selectedMaterial;
            if (_flower is not null) _flower.OnChange += RefreshVisualsWrapper;
            _pr.RefreshVisuals(_flower, _tmp);
        }

        public void Deselect()
        {
            _meshRenderer.material = _defaultMaterial;
            if (_flower is not null) _flower.OnChange -= RefreshVisualsWrapper;
            _pr.DebugClearRender();
        }

        #endregion

        #region CareAndSet

        public void AddFlower(FlowerData flower)
        {
            if (!EnvironmentIsSet) Debug.LogWarning("Environment Is Not Set");
            else
            {
                _flower = new FlowerInstance(flower, _tmp);
                _flower.OnChange += RefreshVisualsWrapper;
                _pr.RefreshVisuals(_flower, _tmp);
            }
        }
        
        public void AddSoil(Environment.SoilType type)
        {
            Debug.Assert(!EnvironmentIsSet, $"Fehler, Soil wurde im Environment Überschrieben!{name}: {_flower}");

            _tmp.soil = type;
            _pr.RefreshVisuals(_flower, _tmp);
        }

        public void ChangeLightType()
        {
            _tmp.lichtkeimer = !_tmp.lichtkeimer;
            _pr.RefreshVisuals(_flower, _tmp);
        }

        public void WaterPlant()
        {
            Debug.Log("Plant is Watered");
            _flower.Water();
        }

        public void Replant()
        {
            TableController.Instance.ReplantFlower(_flower, _tmp);
            
            _flower.OnChange -= RefreshVisualsWrapper;
            _flower = null;
            
            _tmp = new Environment();
        }
        
        private void RefreshVisualsWrapper() => _pr.RefreshVisuals(_flower, _tmp);
        public bool EnvironmentIsSet => _tmp.soil != Environment.SoilType.None;
        public bool IsReplantable => _flower is null ? false : _flower.IsReplantable;

        #endregion
    }
}