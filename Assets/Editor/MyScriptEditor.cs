using Controller;
using UnityEditor;

namespace Editor
{
    [CustomEditor(typeof(LabelHoverEffect))]
    public class MyScriptEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            //do this first to make sure you have the latest version
            serializedObject.Update();
     
            //for each property you want to draw ....
            EditorGUILayout.PropertyField(serializedObject.FindProperty("flag"));
 
            //if you need to do something cute like use a different input type you can do this kind of thing...
            bool flag = serializedObject.FindProperty("flag").boolValue;
            EditorGUILayout.PropertyField(serializedObject.FindProperty(flag ? "labelText" : "contentToShow"));

            //do this last!  it will loop over the properties on your object and apply any it needs to, no if necessary!
            serializedObject.ApplyModifiedProperties();
        }
    }
}