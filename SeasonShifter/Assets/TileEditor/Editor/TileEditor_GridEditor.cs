using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;


//Handles all the inputs on the editor and sends the commands through the TileEditor_Grid.cs script

[CustomEditor(typeof(TileEditor_Grid))]
public class TileEditor_GridEditor : Editor {

    TileEditor_Grid grid;
    private bool constantStroke = false; //If constant stroke is true, tile are painted on mouse over without click

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
            HandleUtility.AddDefaultControl(GUIUtility.GetControlID(FocusType.Passive));

            Event e = Event.current;


            Ray r = Camera.current.ScreenPointToRay(new Vector3(e.mousePosition.x, -e.mousePosition.y + Camera.current.pixelHeight));
            Vector3 mousePos = r.origin;

            //if mouse down or constantstroke on paint tiles
            if (e.isMouse && e.button == 0 && (e.type == EventType.MouseDown || e.type == EventType.MouseDrag || constantStroke)) 
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

            //Check for right mouse click --> enable/disable constant stroke
            if (e.isMouse && e.button == 1 && e.type == EventType.MouseDown)
            {
                constantStroke = !constantStroke;
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

        //Buttons to change editor mode
        if(grid.editorEnabled)
        {
            GUILayout.BeginHorizontal();
            if(GUILayout.Button("Create", GUILayout.Width(60)))
            {
                activeMode = BrushMode.Create;
            }
            if (GUILayout.Button("Delete", GUILayout.Width(60)))
            {
                activeMode = BrushMode.Delete;
            }
            GUILayout.EndHorizontal();
        }

        //Enabled and disables the editor
        string buttonText = "Enable Editor";
        if(grid.editorEnabled) buttonText = "Close Editor";
        
        if (GUILayout.Button(buttonText, GUILayout.Width(255)))
        {
            grid.EnableEditor();
            grid.LoadTiles();              
        }
   
        if(grid.editorEnabled)
        {
            if (GUILayout.Button("Create Collider", GUILayout.Width(255)))
            {
                grid.CreateCollider();
            }

            if (GUILayout.Button("Properties", GUILayout.Width(255)))
            {
                TileEditor_GridWindow window = (TileEditor_GridWindow)EditorWindow.GetWindow(typeof(TileEditor_GridWindow));
                window.Init();
            }
        }


        //Repaints the gui on the editor
        SceneView.RepaintAll();
    }

}
