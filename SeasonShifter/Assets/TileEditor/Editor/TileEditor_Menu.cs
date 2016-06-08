using UnityEngine;
using UnityEditor;
 
public class TileEditor_Menu : EditorWindow
{
    private string objectName = "TileEditor";
    private int levelWidth = 50;
    private int levelHeight = 50;

    [MenuItem("Tools/Add TileEditor")]
    private static void ShowWindow()
    {
        TileEditor_Menu window = (TileEditor_Menu)EditorWindow.GetWindow(typeof(TileEditor_Menu));
        window.Init();
    }

    public void Init()
    {
        this.minSize = new Vector2(200, 90);
    }

    void OnGUI()
    {
        GUILayout.Space(10);
        //Object name
        GUILayout.BeginHorizontal();
            GUILayout.Label("Objectname");
            objectName = EditorGUILayout.TextField(objectName, GUILayout.Width(80));
        GUILayout.EndHorizontal();

        GUILayout.Space(5);
        //Object width and height
        GUILayout.BeginHorizontal();
            GUILayout.Label("Width");
            levelWidth = EditorGUILayout.IntField(levelWidth, GUILayout.Width(40));
            GUILayout.Space(15);
            GUILayout.Label("Height");
            levelHeight = EditorGUILayout.IntField(levelHeight, GUILayout.Width(40));
        GUILayout.EndHorizontal();

        GUILayout.Space(5);
        //Button
        if(GUILayout.Button("Add Object", GUILayout.Width(80)))
        {
            CreateEditorObject();
        }
    }


    void CreateEditorObject()
    {
        GameObject editor = new GameObject(objectName);
        TileEditor_ObjectHandler objectHandler = editor.AddComponent<TileEditor_ObjectHandler>();
        objectHandler.CreateArray(levelWidth, levelHeight);
        editor.AddComponent<TileEditor_DisplayBrushCollection>();
        TileEditor_Grid grid = editor.AddComponent<TileEditor_Grid>();
        grid.InitializeGrid(levelWidth, levelHeight);
    }
}
