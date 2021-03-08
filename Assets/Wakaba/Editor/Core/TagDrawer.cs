using UnityEditor;
using UnityEngine;
namespace Wakaba
{
    [CustomPropertyDrawer(typeof(TagAttribute))] // Tells Unity what to apply this drawer to.
    public class TagDrawer : PropertyDrawer
    {
        /// <summary>The function that renders the property into the inspector.</summary>
        public override void OnGUI(Rect _position, SerializedProperty _property, GUIContent _label)
        {
            // Start drawing this specific instance of the tag property.
            EditorGUI.BeginProperty(_position, _label, _property);

            // Determine if the property was set to nothing by default.
            bool isNotSet = string.IsNullOrEmpty(_property.stringValue);

            // Draw the string as a tag instead of a normal string box.
            _property.stringValue = EditorGUI.TagField(_position, _label,
                isNotSet ? (_property.serializedObject.targetObject as Component).gameObject.tag : _property.stringValue);

            // Stop drawing this specific instance of the tag property.
            EditorGUI.EndProperty();
        }

        /// <summary>Gets the vertical space a single property will take in the inspector.</summary>
        public override float GetPropertyHeight(SerializedProperty _property, GUIContent _label) => EditorGUIUtility.singleLineHeight;
    }
}