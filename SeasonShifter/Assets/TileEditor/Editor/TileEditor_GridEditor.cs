﻿using UnityEngine;
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
    bool changeSurrounding = true;

    Rect areaRect = new Rect(0,0,0,0);
    Rect controlsRect = new Rect (0,0,0,0);
    Rect controlsRect2 = new Rect(0, 0, 0, 0);

    //Selection tool variables
    private Vector3 startingPoint;
    private Vector3 endPoint;
    bool selectionOn;

    int sortingLayerIndex = 0;


    public void OnEnable()
    {
        grid = (TileEditor_Grid)target;
        SceneView.onSceneGUIDelegate += GridUpdate;
    }


    public void GridUpdate(SceneView sceneview)
    {
        guiSections = new Rect[3] { controlsRect, controlsRect2, areaRect }; //An array that contains all the areas occupied by gui

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
                                grid.AddSprite(aligned, changeSurrounding);
                            }
                            else
                            {
                                grid.RemoveSprite(aligned, changeSurrounding);
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
                    grid.SelectArea(startingPoint, endPoint, eraserOn, changeSurrounding);
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
            float buttonWidth = Screen.width / 17;
            float buttonHeight = Screen.height / 20;

            controlsRect = new Rect(10, 10, buttonWidth * 3.2f, buttonHeight * 1.2f);
            controlsRect2 = new Rect(buttonWidth * 4, 10, buttonWidth * 2.15f, buttonHeight * 1.2f);

            GUILayout.BeginArea(controlsRect);
            EditorGUILayout.BeginHorizontal();
            //Set the color of the button
            if(activeMode == BrushMode.Create){ GUI.backgroundColor = grid.selectedButtonBackground; } else { GUI.backgroundColor = grid.defaultButtonBackground; }
            if(GUILayout.Button("Brush", GUILayout.Width(buttonWidth), GUILayout.Height(buttonHeight)))
            {
                activeMode = BrushMode.Create;
            }
            if (activeMode == BrushMode.Fill) { GUI.backgroundColor = grid.selectedButtonBackground; } else { GUI.backgroundColor = grid.defaultButtonBackground; }
            if (GUILayout.Button("Fill", GUILayout.Width(buttonWidth), GUILayout.Height(buttonHeight)))
            {
                activeMode = BrushMode.Fill;
            }
            if (activeMode == BrushMode.Select) { GUI.backgroundColor = grid.selectedButtonBackground; } else { GUI.backgroundColor = grid.defaultButtonBackground; }
            if (GUILayout.Button("Selection", GUILayout.Width(buttonWidth), GUILayout.Height(buttonHeight)))
            {
                activeMode = BrushMode.Select;
            }
            EditorGUILayout.EndHorizontal();
            GUILayout.EndArea();

            GUILayout.BeginArea(controlsRect2);
            EditorGUILayout.BeginHorizontal();
            
            if (activeMode == BrushMode.Create || activeMode == BrushMode.Select)
            {
                //Button to erase or not erase
                string textErase;
                if (eraserOn)
                {
                    textErase = "Not erase";
                    GUI.backgroundColor = grid.defaultButtonBackground;
                }
                else
                {
                    textErase = "erase";
                    GUI.backgroundColor = grid.selectedButtonBackground;
                }
                if (GUILayout.Button(textErase, GUILayout.Width(buttonWidth), GUILayout.Height(buttonHeight)))
                {
                    eraserOn = !eraserOn;
                }

                //Button to prevent change on surrounding tiles
                string textSurrounding;
                if (changeSurrounding)
                {
                    textSurrounding = "Change Surrounding";
                    GUI.backgroundColor = grid.selectedButtonBackground;
                }
                else
                {
                    textSurrounding = "Don't change";
                    GUI.backgroundColor = grid.defaultButtonBackground;
                }
                if (GUILayout.Button(textSurrounding, GUILayout.Width(buttonWidth), GUILayout.Height(buttonHeight)))
                {
                    changeSurrounding = !changeSurrounding;
                }
            }
            EditorGUILayout.EndHorizontal();
            GUILayout.EndArea();

            //Set back background color back
            GUI.backgroundColor = Color.white;

            //---Buttons top right-------------------------------------
            Texture2D image = new Texture2D(2, 2);
            if (TileEditor_BrushCollection.GetActiveBrush() != null)
            {
                image = TileEditor_BrushCollection.GetActiveBrush().thumbnail;
                areaRect = new Rect(sceneview.camera.pixelWidth - 220, 10, 190, 150);
            }
            else if(TileEditor_SpriteCollection.GetActiveSprite() != null)
            {
                image = TileEditor_SpriteCollection.GetActiveSprite().texture;
                areaRect = new Rect(sceneview.camera.pixelWidth - 220, 10, 190, 250);
            }           

            GUILayout.BeginArea(areaRect);
            EditorGUILayout.BeginHorizontal();
                GUILayout.FlexibleSpace();
                GUILayout.Box(image, GUILayout.Width(50), GUILayout.Height(50));
                GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
                //GUILayout.Label("Sorting Layer:");
                GUILayout.Label("Sorting Layer:");
                GUILayout.Space(5);
                grid.sortingLayerIndex = EditorGUILayout.Popup(grid.sortingLayerIndex, grid.sortingLayers);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
                GUILayout.Label("Order in layer:");
                grid.orderInLayer = EditorGUILayout.IntField(grid.orderInLayer);
                if (GUILayout.Button("+")) grid.orderInLayer++;
                if (GUILayout.Button("-")) grid.orderInLayer--;
            EditorGUILayout.EndHorizontal();

            GUILayout.Space(5);

            EditorGUILayout.BeginHorizontal();
                GUILayout.Label("Material:");
                grid.material = EditorGUILayout.ObjectField(grid.material,typeof(Material), true) as Material;
            EditorGUILayout.EndHorizontal();

            GUILayout.Space(5);

            EditorGUILayout.BeginHorizontal();
                GUILayout.Space(60);
                GUILayout.Label("Flip X:");
                grid.flipX = EditorGUILayout.Toggle(grid.flipX);
                GUILayout.Space(3);
                GUILayout.Label("Flip Y:");
                grid.flipY = EditorGUILayout.Toggle(grid.flipY);
            EditorGUILayout.EndHorizontal();

            GUILayout.Space(2);

            if(TileEditor_SpriteCollection.GetActiveSprite() != null)
            {
                EditorGUILayout.Separator();
                GUILayout.Space(2);
                EditorGUILayout.BeginHorizontal();
                    GUILayout.Label("Collider:");
                    GUILayout.Space(5);
                    grid.colliderTypeIndex = EditorGUILayout.Popup(grid.colliderTypeIndex, grid.colliderType);
                EditorGUILayout.EndHorizontal();
                if (grid.colliderTypeIndex != 0)
                {
                    EditorGUILayout.BeginHorizontal();
                    GUILayout.Label("Is trigger");
                    grid.isTrigger = EditorGUILayout.Toggle(grid.isTrigger);
                    EditorGUILayout.EndHorizontal();

                    if(grid.colliderTypeIndex == 1)
                    {
                        grid.boxColliderSize = EditorGUILayout.Vector2Field("Size",grid.boxColliderSize);
                        grid.boxColliderOffset = EditorGUILayout.Vector2Field("Offset", grid.boxColliderOffset);
                    }
                }
            }
            
            GUILayout.EndArea();

            Handles.EndGUI();

            //Check if there is a change in the gui 
            if(GUI.changed)
            {
                grid.SetObjectInformation(); //Set the sprite renderer information
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
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Create Collider", GUILayout.Width(255)))
            {
                grid.CreateCollider(sortingLayerIndex);
            }
            GUILayout.Label("Layer");
            var sortingLayers = SortingLayer.layers;
            string[] sortingLayerNames = new string[sortingLayers.Length];
            for(int i=0; i < sortingLayers.Length; i++)
            {
                sortingLayerNames[i] = sortingLayers[i].name;
            }
            sortingLayerIndex = EditorGUILayout.Popup(sortingLayerIndex, sortingLayerNames);
            EditorGUILayout.EndHorizontal();

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
