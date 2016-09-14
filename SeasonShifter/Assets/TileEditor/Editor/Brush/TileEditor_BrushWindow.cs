using UnityEngine;
using UnityEditor;
using System.Collections;

public class TileEditor_BrushWindow : EditorWindow{

    TileEditor_Brush brush;
    GUIStyle brushGuiStyle;

    public void Init()
    {
        this.minSize = new Vector2(380, 490);
    }

    public void AddBrush()
    {
        brush = TileEditor_BrushCollection.AddBrush(new TileEditor_Brush());
    }

    public void GetBrush()
    {
        brush = TileEditor_BrushCollection.GetActiveBrush();
    }

    void OnGUI()
    {
       if (brush == null) AddBrush();
        GUILayout.Space(10);

        GUILayout.BeginHorizontal();
            GUILayout.Space(20);
            GUILayout.BeginVertical();
                GUILayout.Label("Brush name: ", EditorStyles.boldLabel);
                brush.brushName = EditorGUILayout.TextField(brush.brushName, GUILayout.Width(100));
            GUILayout.EndVertical();
            GUILayout.BeginVertical();
                GUILayout.Label("Thumbnail: ", EditorStyles.boldLabel);
                brush.thumbnail = EditorGUILayout.ObjectField(brush.thumbnail, typeof(Texture2D), true, GUILayout.Width(50), GUILayout.Height(50)) as Texture2D;
        GUILayout.EndVertical();
        GUILayout.EndHorizontal();

        GUILayout.Space(20);
        GUILayout.BeginHorizontal();
            GUILayout.Space(74);
            brush.sprites[0] = EditorGUILayout.ObjectField(brush.sprites[0], typeof(Sprite), true, GUILayout.Width(50), GUILayout.Height(50)) as Sprite;
            brush.sprites[1] = EditorGUILayout.ObjectField(brush.sprites[1], typeof(Sprite), true, GUILayout.Width(50), GUILayout.Height(50)) as Sprite;
            brush.sprites[2] = EditorGUILayout.ObjectField(brush.sprites[2], typeof(Sprite), true, GUILayout.Width(50), GUILayout.Height(50)) as Sprite;
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
            GUILayout.Space(20);
            brush.sprites[0] = EditorGUILayout.ObjectField(brush.sprites[0], typeof(Sprite), true, GUILayout.Width(50), GUILayout.Height(50)) as Sprite;
            brush.sprites[9] = EditorGUILayout.ObjectField(brush.sprites[9], typeof(Sprite), true, GUILayout.Width(50), GUILayout.Height(50)) as Sprite;
            brush.sprites[4] = EditorGUILayout.ObjectField(brush.sprites[4], typeof(Sprite), true, GUILayout.Width(50), GUILayout.Height(50)) as Sprite;
            brush.sprites[10] = EditorGUILayout.ObjectField(brush.sprites[10], typeof(Sprite), true, GUILayout.Width(50), GUILayout.Height(50)) as Sprite;
            brush.sprites[2] = EditorGUILayout.ObjectField(brush.sprites[2], typeof(Sprite), true, GUILayout.Width(50), GUILayout.Height(50)) as Sprite;
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
            GUILayout.Space(20);
            brush.sprites[3] = EditorGUILayout.ObjectField(brush.sprites[3], typeof(Sprite), true, GUILayout.Width(50), GUILayout.Height(50)) as Sprite;
            brush.sprites[4] = EditorGUILayout.ObjectField(brush.sprites[4], typeof(Sprite), true, GUILayout.Width(50), GUILayout.Height(50)) as Sprite;
            brush.sprites[4] = EditorGUILayout.ObjectField(brush.sprites[4], typeof(Sprite), true, GUILayout.Width(50), GUILayout.Height(50)) as Sprite;
            brush.sprites[4] = EditorGUILayout.ObjectField(brush.sprites[4], typeof(Sprite), true, GUILayout.Width(50), GUILayout.Height(50)) as Sprite;
            brush.sprites[5] = EditorGUILayout.ObjectField(brush.sprites[5], typeof(Sprite), true, GUILayout.Width(50), GUILayout.Height(50)) as Sprite;
            //Vertical line
            GUILayout.Space(24);
            brush.sprites[16] = EditorGUILayout.ObjectField(brush.sprites[16], typeof(Sprite), true, GUILayout.Width(50), GUILayout.Height(50)) as Sprite;
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
            GUILayout.Space(20);
            brush.sprites[6] = EditorGUILayout.ObjectField(brush.sprites[6], typeof(Sprite), true, GUILayout.Width(50), GUILayout.Height(50)) as Sprite;
            brush.sprites[11] = EditorGUILayout.ObjectField(brush.sprites[11], typeof(Sprite), true, GUILayout.Width(50), GUILayout.Height(50)) as Sprite;
            brush.sprites[4] = EditorGUILayout.ObjectField(brush.sprites[4], typeof(Sprite), true, GUILayout.Width(50), GUILayout.Height(50)) as Sprite;
            brush.sprites[12] = EditorGUILayout.ObjectField(brush.sprites[12], typeof(Sprite), true, GUILayout.Width(50), GUILayout.Height(50)) as Sprite;
            brush.sprites[8] = EditorGUILayout.ObjectField(brush.sprites[8], typeof(Sprite), true, GUILayout.Width(50), GUILayout.Height(50)) as Sprite;
            //Vertical line
            GUILayout.Space(24);
            brush.sprites[17] = EditorGUILayout.ObjectField(brush.sprites[17], typeof(Sprite), true, GUILayout.Width(50), GUILayout.Height(50)) as Sprite;
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
            GUILayout.Space(74);
            brush.sprites[6] = EditorGUILayout.ObjectField(brush.sprites[6], typeof(Sprite), true, GUILayout.Width(50), GUILayout.Height(50)) as Sprite;
            brush.sprites[7] = EditorGUILayout.ObjectField(brush.sprites[7], typeof(Sprite), true, GUILayout.Width(50), GUILayout.Height(50)) as Sprite;
            brush.sprites[8] = EditorGUILayout.ObjectField(brush.sprites[8], typeof(Sprite), true, GUILayout.Width(50), GUILayout.Height(50)) as Sprite;
            //Vertical line
            GUILayout.Space(78);
            brush.sprites[18] = EditorGUILayout.ObjectField(brush.sprites[18], typeof(Sprite), true, GUILayout.Width(50), GUILayout.Height(50)) as Sprite;
        GUILayout.EndHorizontal();

        //Horizontal line
        GUILayout.Space(24);
        GUILayout.BeginHorizontal();
            GUILayout.Space(128);
            brush.sprites[13] = EditorGUILayout.ObjectField(brush.sprites[13], typeof(Sprite), true, GUILayout.Width(50), GUILayout.Height(50)) as Sprite;
            brush.sprites[14] = EditorGUILayout.ObjectField(brush.sprites[14], typeof(Sprite), true, GUILayout.Width(50), GUILayout.Height(50)) as Sprite;
            brush.sprites[15] = EditorGUILayout.ObjectField(brush.sprites[15], typeof(Sprite), true, GUILayout.Width(50), GUILayout.Height(50)) as Sprite;
        GUILayout.EndHorizontal();

        //-------Slope Tiles----------------
        GUILayout.Space(10);
        GUILayout.Label("Slope Tiles: ", EditorStyles.boldLabel);
        GUILayout.Space(5);
        GUILayout.BeginHorizontal();
        GUILayout.Space(74);
        brush.sprites[19] = EditorGUILayout.ObjectField(brush.sprites[19], typeof(Sprite), true, GUILayout.Width(50), GUILayout.Height(50)) as Sprite;
        brush.sprites[20] = EditorGUILayout.ObjectField(brush.sprites[20], typeof(Sprite), true, GUILayout.Width(50), GUILayout.Height(50)) as Sprite;
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.Space(20);
            brush.sprites[19] = EditorGUILayout.ObjectField(brush.sprites[19], typeof(Sprite), true, GUILayout.Width(50), GUILayout.Height(50)) as Sprite;
            brush.sprites[23] = EditorGUILayout.ObjectField(brush.sprites[23], typeof(Sprite), true, GUILayout.Width(50), GUILayout.Height(50)) as Sprite;
            brush.sprites[24] = EditorGUILayout.ObjectField(brush.sprites[24], typeof(Sprite), true, GUILayout.Width(50), GUILayout.Height(50)) as Sprite;
            brush.sprites[20] = EditorGUILayout.ObjectField(brush.sprites[20], typeof(Sprite), true, GUILayout.Width(50), GUILayout.Height(50)) as Sprite;
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.Space(20);
        brush.sprites[21] = EditorGUILayout.ObjectField(brush.sprites[21], typeof(Sprite), true, GUILayout.Width(50), GUILayout.Height(50)) as Sprite;
        brush.sprites[25] = EditorGUILayout.ObjectField(brush.sprites[25], typeof(Sprite), true, GUILayout.Width(50), GUILayout.Height(50)) as Sprite;
        brush.sprites[26] = EditorGUILayout.ObjectField(brush.sprites[26], typeof(Sprite), true, GUILayout.Width(50), GUILayout.Height(50)) as Sprite;
        brush.sprites[22] = EditorGUILayout.ObjectField(brush.sprites[22], typeof(Sprite), true, GUILayout.Width(50), GUILayout.Height(50)) as Sprite;
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.Space(74);
        brush.sprites[21] = EditorGUILayout.ObjectField(brush.sprites[21], typeof(Sprite), true, GUILayout.Width(50), GUILayout.Height(50)) as Sprite;
        brush.sprites[22] = EditorGUILayout.ObjectField(brush.sprites[22], typeof(Sprite), true, GUILayout.Width(50), GUILayout.Height(50)) as Sprite;
        GUILayout.EndHorizontal();


        GUILayout.Space(20);
        GUILayout.BeginHorizontal();
            GUILayout.Space(160);
            if (GUILayout.Button("Dispose", GUILayout.Width(100)))
            {
                TileEditor_BrushCollection.RemoveBrush(brush);
                this.Close();
            }
            if (GUILayout.Button("Save", GUILayout.Width(100)))
            {
                foreach(TileEditor_Brush existingBrush in TileEditor_BrushCollection.brushContainer)
                {
                    existingBrush.ConvertToPng();
                }
                TileEditor_BrushCollection.SaveBrushCollection();
                this.Close();
            } 
        GUILayout.EndHorizontal();
    }
}
