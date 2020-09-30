#if UNITY_EDITOR
using System;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(FilePathAttribute))]
public class FilePathDrawer : PropertyDrawer 
{
    // Draw the property inside the given rect
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        // First get the attribute since it contains the range for the slider
        FilePathAttribute attr = (FilePathAttribute)attribute;
        EditorGUI.BeginProperty(position, label, property);

        var padding = 10;

        //EditorGUILayout.BeginHorizontal();
        var btnWidth = position.height * 3;

        Rect rect = position;
        rect.width -= btnWidth + padding;
        property.stringValue = EditorGUI.TextField(rect, property.name, property.stringValue);

        Rect r2 = new Rect(rect.width + padding * 2, rect.y, btnWidth, rect.height);
        if(GUI.Button(r2, "find"))
        {
            var fullPath = EditorUtility.OpenFilePanel(property.name, "", attr.Ext);
            if(string.IsNullOrEmpty(fullPath) == false)
            {
                var uriA = new Uri(Application.dataPath);
                var uriB = new Uri(fullPath);
                var relativePath = uriA.MakeRelativeUri(uriB);
                property.stringValue = relativePath.ToString();
                property.serializedObject.ApplyModifiedProperties();
            }
        }

        EditorGUI.EndProperty();
    }
}
#endif