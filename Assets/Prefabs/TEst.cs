using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TileScriptEditor))] // Replace 'TileScript' with the name of your tile script
public class TileScriptEditor : Editor
{
    private SerializedProperty tilePrefabProperty;

    private void OnEnable()
    {
        // Initialize serialized property for accessing the tile prefab
        tilePrefabProperty = serializedObject.FindProperty("tilePrefab");
    }

    public override void OnInspectorGUI()
    {
        // Update serialized object's representation
        serializedObject.Update();

        // Display object field to select tile prefab
        EditorGUILayout.PropertyField(tilePrefabProperty, true);

        // Apply changes to serialized object
        serializedObject.ApplyModifiedProperties();
    }
}