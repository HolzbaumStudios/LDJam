using UnityEngine;
using UnityEditor;
using System.Collections;


//Handles all the inputs on the editor and sends the commands through the TileEditor_Grid.cs script

[CustomEditor(typeof(TileEditor_Grid))]
public class TileEditor_GridEditor : Editor {

    TileEditor_Grid grid;
    enum BrushMode { Create, Delete};
    BrushMode activeMode = BrushMode.Create;

    public void OnEnable()
    {
        grid = (TileEditor_Grid)target;
        SceneView.onSceneGUIDelegate += GridUpdate;
    }

    public void GridUpdate(SceneView sceneview)
    {
        if (grid.editorEnabled)
        {
            Event e = Event.current;

            Ray r = Camera.current.ScreenPointToRay(new Vector3(e.mousePosition.x, -e.mousePosition.y + Camera.current.pixelHeight));
            Vector3 mousePos = r.origin;

            if (e.isMouse && e.button == 0 && (e.type == EventType.MouseDown || e.type == EventType.MouseDrag)) // && e.type == EventType.MouseDown
            {

                Vector3 aligned = new Vector3(Mathf.Floor(mousePos.x / grid.lineWidth) * grid.lineWidth + grid.lineWidth / 2.0f,
                                    Mathf.Floor(mousePos.y / grid.lineHeight) * grid.lineHeight + grid.lineHeight / 2.0f, 0.0f);

                switch (activeMode)
                {
                    case BrushMode.Create:
                        grid.AddSprite(aligned);
                        break;
                    case BrushMode.Delete:
                        grid.RemoveSprite(aligned);
                        break;      
                }
            }
        }
    }

    public void OnDisable()
    {
        SceneView.onSceneGUIDelegate -= GridUpdate;
    }

    public override void OnInspectorGUI()
    {
        GUILayout.BeginHorizontal();
        GUILayout.Label(" Line Width ");
        grid.lineWidth = EditorGUILayout.FloatField(grid.lineWidth, GUILayout.Width(50));
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.Label(" Line Height ");
        grid.lineHeight = EditorGUILayout.FloatField(grid.lineHeight, GUILayout.Width(50));
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.Label(" Grid Width ");
        grid.gridWidth = EditorGUILayout.FloatField(grid.gridWidth, GUILayout.Width(50));
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.Label(" Grid Height ");
        grid.gridHeight = EditorGUILayout.FloatField(grid.gridHeight, GUILayout.Width(50));
        GUILayout.EndHorizontal();

        //Opens a separate property window
        if (GUILayout.Button("Open Grid Window", GUILayout.Width(255)))
        {
            TileEditor_GridWindow window = (TileEditor_GridWindow)EditorWindow.GetWindow(typeof(TileEditor_GridWindow));
            window.Init();
        }

        //Enabled and disables the editor
        if (GUILayout.Button("Enable / Disable Editor", GUILayout.Width(255)))
        {
            grid.EnableEditor();              
        }

        //Repaints the gui on the editor
        SceneView.RepaintAll();
    }
}
