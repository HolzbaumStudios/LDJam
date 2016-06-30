using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;


//Handles all the inputs on the editor and sends the commands through the TileEditor_Grid.cs script

[CustomEditor(typeof(TileEditor_Grid))]
public class TileEditor_GridEditor : Editor {

    TileEditor_Grid grid;
    private bool constantStroke = false; //If constant stroke is true, tile are painted on mouse over without click
    Rect[] guiSections; //An array that contains all the areas occupied by gui

    enum BrushMode { Create, Fill, Select};
    BrushMode activeMode = BrushMode.Create;
    bool eraserOn = false;
    

    //Selection tool variables
    private Vector3 startingPoint;
    private Vector3 endPoint;
    bool selectionOn;


    public void OnEnable()
    {
        grid = (TileEditor_Grid)target;
        SceneView.onSceneGUIDelegate += GridUpdate;
    }


    public void GridUpdate(SceneView sceneview)
    {
        guiSections = new Rect[3] { new Rect(0, 0, 210, 50), new Rect(10, 50, 70, 30), new Rect(sceneview.camera.pixelWidth - 200, 10, 180, 100) }; //An array that contains all the areas occupied by gui

        if (grid.editorEnabled)
        {
            HandleUtility.AddDefaultControl(GUIUtility.GetControlID(FocusType.Passive));

            Event e = Event.current;

            Ray r = Camera.current.ScreenPointToRay(new Vector3(e.mousePosition.x, -e.mousePosition.y + Camera.current.pixelHeight));
            Vector3 mousePos = r.origin;

            bool insideGUI = false;
            if (MouseInRect(e.mousePosition)) insideGUI = true;

            Vector3 aligned = new Vector3(Mathf.Floor(mousePos.x / grid.lineWidth) * grid.lineWidth + grid.lineWidth / 2.0f,
                                    Mathf.Floor(mousePos.y / grid.lineHeight) * grid.lineHeight + grid.lineHeight / 2.0f, 0.0f);

            //if mouse down or constantstroke on paint tiles
            if (e.isMouse && e.button == 0 && (e.type == EventType.MouseDown || e.type == EventType.MouseDrag || constantStroke) && !insideGUI) 
            {
                if (!selectionOn)
                {
                    switch (activeMode)
                    {
                        case BrushMode.Create:
                            if (!eraserOn)
                            {
                                grid.AddSprite(aligned);
                            }
                            else
                            {
                                grid.RemoveSprite(aligned);
                            }
                            break;
                        case BrushMode.Fill:
                            grid.FillArea(aligned);
                            break;
                        case BrushMode.Select:
                            startingPoint = aligned;
                            selectionOn = true;                            
                            break;
                    }
                }
            }
            else if(selectionOn) //If selection tool is active and mouse is being pressed down
            {
                endPoint = aligned;
                float x1, x2, y1, y2; //Additional value that has to be added to the rect
                if (startingPoint.x > endPoint.x) { x1 = 0.5f; x2 = -1.5f; } else { x1 = -0.5f; x2 = 0.5f; }
                if (startingPoint.y > endPoint.y) { y1 = 0.5f; y2 = -1.5f; } else { y1 = -0.5f; y2 = 0.5f; }
                Rect textureRect = new Rect(startingPoint.x+x1,startingPoint.y+y1,endPoint.x-startingPoint.x+x2,endPoint.y - startingPoint.y+y2);
                Graphics.DrawTexture(textureRect, new Texture2D(1,1));
                //Debug.Log("Start Point: " + startingPoint + " End Point: " + endPoint);
                if (e.isMouse && e.button == 0 && e.type == EventType.MouseUp)
                {
                    selectionOn = false;
                    grid.SelectArea(startingPoint, endPoint, eraserOn);
                }
            }

            //Check for right mouse click --> enable/disable constant stroke
            if (e.isMouse && e.button == 1 && e.type == EventType.MouseDown)
            {
                constantStroke = !constantStroke;
            }

            ////////GUI/////////////
            Handles.BeginGUI();
            //Buttons top left-------------
            if (GUI.Button(new Rect(10, 10, 70, 30), "Brush"))
            {
                activeMode = BrushMode.Create;
            }
            if (GUI.Button(new Rect(80, 10, 70, 30), "Fill"))
            {
                activeMode = BrushMode.Fill;
            }
            if (GUI.Button(new Rect(150, 10, 70, 30), "Selection"))
            {
                activeMode = BrushMode.Select;
            }

            if(activeMode == BrushMode.Create || activeMode == BrushMode.Select)
            {
                if (eraserOn)
                {
                    if (GUI.Button(new Rect(10, 50, 70, 30), "Not Erase"))
                    {
                        eraserOn = false;
                    }
                }
                else
                {
                    if (GUI.Button(new Rect(10, 50, 70, 30), "Erase"))
                    {
                        eraserOn = true;
                    }
                }
            }


            //Buttons top right------------------
            Rect areaRect = new Rect(sceneview.camera.pixelWidth - 200, 10, 180, 100);
            GUILayout.BeginArea(areaRect);
            GUILayout.BeginHorizontal();
                //GUILayout.Label("Sorting Layer:");
            grid.sortingLayerIndex = EditorGUI.Popup(new Rect(0, 0, 120, 15), grid.sortingLayerIndex, grid.sortingLayers);
            GUILayout.EndHorizontal();

               // GUILayout.Label("Material:");
                //GUILayout.Label("Order in layer:");
            GUILayout.EndArea();

            Handles.EndGUI();

            //Check if there is a change in the gui 
            if(GUI.changed)
            {
                //Sorting layers
                if(grid.sortingLayerIndex==0)
                {
                    Debug.Log("Add new sorting layer..");
                    grid.SetSortingLayer();
                }
                else
                {
                    Debug.Log("Index: " + grid.sortingLayerIndex);
                    grid.SetSortingLayer();
                }
            }

        }
    }

    public void OnDisable()
    {
        SceneView.onSceneGUIDelegate -= GridUpdate;
    }

    //Check if the mouse position is over a GUI Element
    bool MouseInRect(Vector2 mousePosition)
    {
        foreach(Rect rect in guiSections)
        {
            if (rect.Contains(mousePosition)) return true;
        }
        return false;
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
                eraserOn = false;
            }
            if (GUILayout.Button("Delete", GUILayout.Width(60)))
            {
                eraserOn = true;
            }
            if (GUILayout.Button("Fill", GUILayout.Width(60)))
            {
                activeMode = BrushMode.Fill;
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
