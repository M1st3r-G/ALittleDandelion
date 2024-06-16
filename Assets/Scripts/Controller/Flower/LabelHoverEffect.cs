using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Controller
{
    [RequireComponent(typeof(Collider))]
    public class LabelHoverEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        private TextMeshProUGUI _outputField;
        
        public bool flag;
        public TextMeshProUGUI labelText;
        public string contentToShow;
        
        private void Awake()
        {
            _outputField = GameObject.FindGameObjectWithTag("LabelText").GetComponent<TextMeshProUGUI>();
            labelText = GetComponentInChildren<TextMeshProUGUI>();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            string tmp = flag ? labelText.text : contentToShow;
            _outputField.text = tmp;
            Debug.Log($"{_outputField.text} is displayed, {tmp} should be displayed");
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _outputField.text = "";
        }
    }
    
    [CustomEditor(typeof(LabelHoverEffect))]
    public class MyScriptEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            //do this first to make sure you have the latest version
            serializedObject.Update();
     
            //for each property you want to draw ....
            EditorGUILayout.PropertyField(serializedObject.FindProperty("flag"));
 
            //if you need to do something cute like use a different input type you can do this kind of thing...
            bool flag = serializedObject.FindProperty("flag").boolValue;

            if (flag)
            {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("labelText"));
            }
            else EditorGUILayout.PropertyField(serializedObject.FindProperty("contentToShow"));
            
            //do this last!  it will loop over the properties on your object and apply any it needs to, no if necessary!
            serializedObject.ApplyModifiedProperties();
        }
    }
}