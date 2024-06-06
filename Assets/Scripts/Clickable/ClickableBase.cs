using UnityEngine;
using UnityEngine.EventSystems;

namespace Clickable
{
    [RequireComponent(typeof(MeshFilter))]
    [RequireComponent(typeof(MeshRenderer))]
    [RequireComponent(typeof(Collider))]
    public abstract class ClickableBase : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private Material hoverMaterial;
        private MeshRenderer _hoverMesh;

        protected void Awake()
        {
            GameObject tmp = new GameObject($"{name}Hover");
            tmp.transform.SetParent(transform);
            tmp.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
            tmp.transform.localScale = Vector3.one * 1.1f;

            MeshFilter mf = tmp.AddComponent<MeshFilter>();
            mf.mesh = gameObject.GetComponent<MeshFilter>().mesh;
            
            _hoverMesh = tmp.AddComponent<MeshRenderer>();
            _hoverMesh.material = hoverMaterial;
            
            tmp.SetActive(false);
        }

        public abstract void OnPointerClick(PointerEventData eventData);
        
        public void OnPointerEnter(PointerEventData eventData)
        {
            _hoverMesh.gameObject.SetActive(true);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _hoverMesh.gameObject.SetActive(false);
        }
    }
}