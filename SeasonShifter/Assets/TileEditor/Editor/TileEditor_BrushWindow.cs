using UnityEngine;
using UnityEditor;
using System.Collections;

public class TileEditor_BrushWindow : EditorWindow{

    TileEditor_BrushCollection brushCollection;
    TileEditor_Brush brush;

    public void Init()
    {
        brushCollection = (TileEditor_BrushCollection)FindObjectOfType(typeof(TileEditor_BrushCollection));
        brush = new TileEditor_Brush();
    }

    void OnGUI()
    {

        GUILayout.BeginHorizontal();
            brush.sprites[0] = EditorGUILayout.ObjectField(brush.sprites[0],typeof(Sprite),true, GUILayout.Width(50)) as Sprite;
        GUILayout.EndHorizontal();
    }
}
