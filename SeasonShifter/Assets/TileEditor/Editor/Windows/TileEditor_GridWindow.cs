using UnityEngine;
using UnityEditor;
using System.Collections;

public class TileEditor_GridWindow : EditorWindow {

    TileEditor_Grid grid;

    public void Init()
    {
        grid = (TileEditor_Grid)FindObjectOfType(typeof(TileEditor_Grid));
    }

    void OnGUI()
    {
        EditorGUILayout.LabelField("Grid Color");
        grid.color = EditorGUILayout.ColorField(grid.color, GUILayout.Width(200));

        EditorGUILayout.LabelField("Button Color");
        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Default");
        grid.defaultButtonBackground = EditorGUILayout.ColorField(grid.defaultButtonBackground, GUILayout.Width(100));
        EditorGUILayout.Space();
        GUILayout.Label("Selected");
        grid.selectedButtonBackground = EditorGUILayout.ColorField(grid.selectedButtonBackground, GUILayout.Width(100));
        EditorGUILayout.EndHorizontal();
    }
}
