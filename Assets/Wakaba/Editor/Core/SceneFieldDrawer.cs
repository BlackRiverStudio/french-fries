using UnityEditor;
using UnityEngine;
namespace Wakaba
{
    [CustomPropertyDrawer(typeof(SceneFieldAttribute))] // Tells Unity what to apply this drawer to.


                                               public class SceneFieldDrawer : PropertyDrawer
    {       
        /// <summary>The function that renders the property into the inspector.</summary>
        public override void OnGUI(Rect _position, SerializedProperty _property, GUIContent _label)
        {
            // Start drawing this specific instance of the tag property.
            EditorGUI.BeginProperty(_position, _label, _property);

            // Load the scene currently set in the inspector.
            var oldScene = AssetDatabase.LoadAssetAtPath<SceneAsset>(_property.stringValue);

            // Check if anything has changed in the inspector.
            EditorGUI.BeginChangeCheck();

            // Draw the scene field as an object field with the sceneAsset type.
            var newScene = EditorGUI.ObjectField(_position, _label, oldScene, typeof(SceneAsset), false) as SceneAsset;

            // Did anything actually change in the inspector?
            if (EditorGUI.EndChangeCheck())
            {
                // Set the string value to the path of the string.
                string path = AssetDatabase.GetAssetPath(newScene);
                _property.stringValue = path;
            }

            // Stop drawing this specific instance of the tag property.
            EditorGUI.EndProperty();
        }

        /// <summary>Gets the vertical space a single property will take in the inspector.</summary>
        public override float GetPropertyHeight(SerializedProperty _property, GUIContent _label) => EditorGUIUtility.singleLineHeight;
    }
}