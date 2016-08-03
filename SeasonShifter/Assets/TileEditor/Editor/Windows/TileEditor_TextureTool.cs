using UnityEngine;
using UnityEditor;
using System.Collections;

public class TileEditor_TextureTool : EditorWindow {

    public bool textureToolEnabled { get; set; }

    private enum TextureToolMode { Raise_Lower, Paint}
    private TextureToolMode textureMode = TextureToolMode.Raise_Lower;

    public Color defaultButtonBackground = new Color32(255, 255, 255, 255);
    public Color selectedButtonBackground = new Color32(100, 200, 130, 255);

    private int sortingLayerIndex = 0;



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

    public void OnEnable()
    {
        SceneView.onSceneGUIDelegate += TextureUpdate;
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
            if (GUILayout.Button("Close TextureTool"))
                textureToolEnabled = false;

            EditorGUILayout.BeginHorizontal();
            if (textureMode == TextureToolMode.Raise_Lower) { GUI.backgroundColor = selectedButtonBackground; } else { GUI.backgroundColor = defaultButtonBackground; }
            if (GUILayout.Button("Raise/Lower"))
                textureMode = TextureToolMode.Raise_Lower;
            if (textureMode == TextureToolMode.Paint) { GUI.backgroundColor = selectedButtonBackground; } else { GUI.backgroundColor = defaultButtonBackground; }
            if (GUILayout.Button("Paint"))
                textureMode = TextureToolMode.Paint;
            EditorGUILayout.EndHorizontal();
            GUI.backgroundColor = defaultButtonBackground;

            //Choose the sorting layer
            GUILayout.Label("Layer");
            var sortingLayers = SortingLayer.layers;
            string[] sortingLayerNames = new string[sortingLayers.Length];
            for (int i = 0; i < sortingLayers.Length; i++)
            {
                sortingLayerNames[i] = sortingLayers[i].name;
            }
            sortingLayerIndex = EditorGUILayout.Popup(sortingLayerIndex, sortingLayerNames);

        }
    }

    //Check for mouse gestures
    public void TextureUpdate(SceneView sceneview)
    {
        if (textureToolEnabled)
        {
            HandleUtility.AddDefaultControl(GUIUtility.GetControlID(FocusType.Passive));

            Event e = Event.current;

            Ray r = Camera.current.ScreenPointToRay(new Vector3(e.mousePosition.x, -e.mousePosition.y + Camera.current.pixelHeight, 1000));
            Vector3 mousePos = r.origin;

            
        }
    }
}
