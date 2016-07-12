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
        grid.color = EditorGUILayout.ColorField(grid.color, GUILayout.Width(200));
    }
}
