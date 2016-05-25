using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(TileEditor_Grid))]
public class TileEditor_GridEditor : Editor {

    TileEditor_Grid grid;

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

            if (e.isMouse && e.button == 0 && e.type == EventType.MouseDown)
            {
                Debug.Log("Mouseclick!");
                GameObject tile;
                if (Selection.activeObject)
                {
                    tile = (GameObject)Instantiate(Selection.activeObject);
                    Vector3 aligned = new Vector3(Mathf.Floor(mousePos.x / grid.width) * grid.width + grid.width / 2.0f,
                                      Mathf.Floor(mousePos.y / grid.height) * grid.height + grid.height / 2.0f, 0.0f);
                    tile.transform.position = aligned;
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
        GUILayout.Label(" Grid Width ");
        grid.width = EditorGUILayout.FloatField(grid.width, GUILayout.Width(50));
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.Label(" Grid Height ");
        grid.height = EditorGUILayout.FloatField(grid.height, GUILayout.Width(50));
        GUILayout.EndHorizontal();

        //Opens a separate property window
        if (GUILayout.Button("Open Grid Window", GUILayout.Width(255)))
        {
            TileEditor_GridWindow window = (TileEditor_GridWindow)EditorWindow.GetWindow(typeof(TileEditor_GridWindow));
            window.Init();
        }

        //Enabled and disables the editor
        if (GUILayout.Button("Enable / Diabled Editor", GUILayout.Width(255)))
        {
            grid.editorEnabled = !grid.editorEnabled;
        }

        //Repaints the gui on the editor
        SceneView.RepaintAll();
    }
}
