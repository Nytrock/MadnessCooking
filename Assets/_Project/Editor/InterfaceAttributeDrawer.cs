using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(InterfaceAttribute))]
public sealed class InterfaceAttributeDrawer : PropertyDrawer {
    public override void OnGUI(
        Rect position,
        SerializedProperty property,
        GUIContent label) {
        var attribute = (InterfaceAttribute)base.attribute;

        if (property.propertyType != SerializedPropertyType.ObjectReference) {
            EditorGUI.PropertyField(position, property, label);

            EditorGUI.HelpBox(new Rect(position.x, position.y + EditorGUI.GetPropertyHeight(property, label) + 2,
                position.width, EditorGUIUtility.singleLineHeight * 2),
                $"[Interface] can only be used with UnityEngine.Object references.", MessageType.Error);
            return;
        }

        EditorGUI.BeginProperty(position, label, property);

        var currentObject = property.objectReferenceValue;

        var selectedObject = EditorGUI.ObjectField(position, label, currentObject, typeof(Object), true);
        if (selectedObject == null) {
            property.objectReferenceValue = null;
        } else if (attribute.InterfaceType.IsAssignableFrom(selectedObject.GetType())) {
            property.objectReferenceValue = selectedObject;
        } else {
            Debug.LogWarning($"Object '{selectedObject.name}' does not implement {attribute.InterfaceType.FullName}.");
        }

        EditorGUI.EndProperty();
    }
}