using UnityEngine;
using UnityEditor;
using System.Collections;

public class TileEditor_TextureTool : EditorWindow {

    public bool textureToolEnabled { get; set; }

    private enum TextureToolMode { Raise_Lower, Paint}
    private TextureToolMode textureMode = TextureToolMode.Raise_Lower;


    [MenuItem("Tools/TileEditor/TextureTool")]
    private static void ShowWindow()
    {
        TileEditor_TextureTool window = (TileEditor_TextureTool)EditorWindow.GetWindow(typeof(TileEditor_TextureTool));
        window.Init();
    }

    void Init()
    {
        this.minSize = new Vector2(150, 150);
    }

    void OnGUI()
    {
        if(!textureToolEnabled)
        {
            if (GUILayout.Button("Start TextureTool"))
                textureToolEnabled = true;

        }
        else
        {
            HandleUtility.AddDefaultControl(GUIUtility.GetControlID(FocusType.Passive));

            if (GUILayout.Button("Close TextureTool"))
                textureToolEnabled = false;

            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Raise/Lower"))
                textureMode = TextureToolMode.Raise_Lower;
            if (GUILayout.Button("Paint"))
                textureMode = TextureToolMode.Paint;
            EditorGUILayout.EndHorizontal();
        }
    }
}
