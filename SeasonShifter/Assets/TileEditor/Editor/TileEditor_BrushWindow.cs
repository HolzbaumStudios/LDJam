using UnityEngine;
using UnityEditor;
using System.Collections;

public class TileEditor_BrushWindow : EditorWindow{

    TileEditor_BrushCollection brushCollection;
    TileEditor_Brush brush;

    public void Init()
    {
        brushCollection = (TileEditor_BrushCollection)FindObjectOfType(typeof(TileEditor_BrushCollection));
        

    }

    public void AddBrush()
    {
        brush = brushCollection.AddBrush(new TileEditor_Brush());
    }

    void OnGUI()
    {
       if (brush == null) AddBrush();

        GUILayout.BeginHorizontal();
            brush.sprites[0] = EditorGUILayout.ObjectField(brush.sprites[0],typeof(Sprite),true, GUILayout.Width(50), GUILayout.Height(50)) as Sprite;
            brush.sprites[1] = EditorGUILayout.ObjectField(brush.sprites[1], typeof(Sprite), true, GUILayout.Width(50), GUILayout.Height(50)) as Sprite;
            brush.sprites[2] = EditorGUILayout.ObjectField(brush.sprites[2], typeof(Sprite), true, GUILayout.Width(50), GUILayout.Height(50)) as Sprite;
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        brush.sprites[3] = EditorGUILayout.ObjectField(brush.sprites[3], typeof(Sprite), true, GUILayout.Width(50), GUILayout.Height(50)) as Sprite;
        brush.sprites[4] = EditorGUILayout.ObjectField(brush.sprites[4], typeof(Sprite), true, GUILayout.Width(50), GUILayout.Height(50)) as Sprite;
        brush.sprites[5] = EditorGUILayout.ObjectField(brush.sprites[5], typeof(Sprite), true, GUILayout.Width(50), GUILayout.Height(50)) as Sprite;
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        brush.sprites[6] = EditorGUILayout.ObjectField(brush.sprites[6], typeof(Sprite), true, GUILayout.Width(50), GUILayout.Height(50)) as Sprite;
        brush.sprites[7] = EditorGUILayout.ObjectField(brush.sprites[7], typeof(Sprite), true, GUILayout.Width(50), GUILayout.Height(50)) as Sprite;
        brush.sprites[8] = EditorGUILayout.ObjectField(brush.sprites[8], typeof(Sprite), true, GUILayout.Width(50), GUILayout.Height(50)) as Sprite;
        GUILayout.EndHorizontal();

        if (GUILayout.Button("Close window!", GUILayout.Width(100)))
        {
            this.Close();
        }
    }
}
