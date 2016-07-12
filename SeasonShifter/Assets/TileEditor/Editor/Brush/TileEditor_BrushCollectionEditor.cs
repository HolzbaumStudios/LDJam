using UnityEngine;
using UnityEditor;
using System.Collections;
using System.IO;

[CustomEditor(typeof(TileEditor_DisplayBrushCollection))]
public class TileEditor_BrushCollectionEditor : Editor {

    Vector2 scrollPosition = Vector2.zero; //Brushes
    Vector2 scrollPosition2 = Vector2.zero; //Sprites
    GUIStyle labelStyle;
    GUIStyle buttonStyle;
    Texture2D splitterTexture;

    public override void OnInspectorGUI()
    {
        //Define style
        labelStyle = new GUIStyle(GUI.skin.label);
        labelStyle.alignment = TextAnchor.MiddleCenter;
        buttonStyle = new GUIStyle(GUI.skin.button);
        buttonStyle.normal.background = null;

        int brushCount = TileEditor_BrushCollection.brushContainer.Count;
        //Rect scrollview = GUILayoutUtility.GetRect(10,10,150,70);
        //Rect viewRect = new Rect(0, 0, 60 * brushCount, 70);
        scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition, true, false, GUILayout.Width(Screen.width), GUILayout.Height(110));
        EditorGUILayout.BeginHorizontal();
            for(int i = 0; i < brushCount; i++)
            {
                EditorGUILayout.BeginVertical();
                    GUILayout.Space(10);
                    TileEditor_Brush brush = TileEditor_BrushCollection.brushContainer[i];
                    int x = (5 + 5 * i) + 50 * i;
                    GUIStyle tempStyle = buttonStyle;
                    if (brush == TileEditor_BrushCollection.GetActiveBrush())
                    {
                        tempStyle.normal = GUI.skin.button.active;
                    }
                    else
                    {
                        tempStyle.normal.background = null;
                    }
                    if (GUILayout.Button(brush.thumbnail, tempStyle, GUILayout.Width(50), GUILayout.Height(50)))
                    {
                        TileEditor_BrushCollection.ChangeActiveBrush(brush);
                        TileEditor_SpriteCollection.SetSpriteNull();
                    }
                    GUILayout.Label(brush.GetBrushName(), labelStyle, GUILayout.Width(50), GUILayout.Height(20));
                EditorGUILayout.EndVertical();                
            }
        GUILayout.Space(20);
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.EndScrollView();

        //Buttons
        GUILayout.Space(10);

        //if a brush is selected add buttons to delete and modify
        if (TileEditor_BrushCollection.GetActiveBrush() != null)
        {
            EditorGUILayout.BeginHorizontal();

            if (GUILayout.Button("Delete", GUILayout.Width(80)))
            {
                TileEditor_BrushCollection.RemoveBrush(TileEditor_BrushCollection.GetActiveBrush());
            }

            if (GUILayout.Button("Modify", GUILayout.Width(80)))
            {
                TileEditor_BrushWindow window = (TileEditor_BrushWindow)EditorWindow.GetWindow(typeof(TileEditor_BrushWindow));
                window.GetBrush();
                window.Init();
            }

            //Exports a brush

            if (GUILayout.Button("Export", GUILayout.Width(80)))
            {
                TileEditor_SaveLoad.SaveSingleBrush();
            }

            EditorGUILayout.EndHorizontal();
        }

        GUILayout.Space(5);

        EditorGUILayout.BeginHorizontal();
            //Opens window to add brush
            if (GUILayout.Button("+ Brush", GUILayout.Width(80)))
            {
                TileEditor_BrushWindow window = (TileEditor_BrushWindow)EditorWindow.GetWindow(typeof(TileEditor_BrushWindow));
                window.Init();
                window.AddBrush();
            }

            //Imports a brush
            if (GUILayout.Button("Import", GUILayout.Width(80)))
            {
                TileEditor_SaveLoad.LoadSingleBrush();
            }

        EditorGUILayout.EndHorizontal();
        GUILayout.Space(20);

        //SPLITTER------------------------------------------------------
        GUIStyle splitter = new GUIStyle();
        Color color = new Color32(130, 130, 130, 255);
        if (EditorGUIUtility.isProSkin) color = new Color32(80,80,80,255);
        if (splitterTexture == null) //If texture is null, create one
        {
            splitterTexture = new Texture2D(2, 2);
            Color[] pixelColors = new Color[4];
            for(int i=0; i < pixelColors.Length; i++)
            {
                pixelColors[i] = color; 
            }
            splitterTexture.SetPixels(pixelColors);
        }
        splitter.normal.background = splitterTexture;
        splitter.stretchWidth = true;
        
        

        GUILayout.Box("", splitter, GUILayout.Width(Screen.width*0.94f), GUILayout.Height(2));

        //SPRITES-------------------------------------------------------

        TileEditor_Sprites spriteGroup = TileEditor_SpriteCollection.GetActiveSpriteGroup();


        GUILayout.Space(10);

        string[] groupNames = TileEditor_SpriteCollection.GetGroupNames();

        EditorGUILayout.BeginHorizontal();
            TileEditor_SpriteCollection.activeGroupIndex = EditorGUILayout.Popup(TileEditor_SpriteCollection.activeGroupIndex, groupNames);
            if (spriteGroup != null)
            {
                if (GUILayout.Button("Delete")) TileEditor_SpriteCollection.DeleteGroup();
            }
            if (GUILayout.Button("Add Group")) OpenNewGroupWindow();
        EditorGUILayout.EndHorizontal();

        if(spriteGroup!=null)
        { 
            int countSprites = 0;
            if (spriteGroup != null && spriteGroup.spriteGroup != null) countSprites = spriteGroup.spriteGroup.Count;
            Rect boxRect;
        
            if (countSprites > 0)
            {
                int buttonWidth = 50;
                int columns = (int)((Screen.width * 0.9f) / (buttonWidth));

                EditorGUILayout.BeginVertical();
                scrollPosition2 = EditorGUILayout.BeginScrollView(scrollPosition2, false, false);

                for (int i = 0; i < countSprites; i++)
                {
                    EditorGUILayout.BeginHorizontal();
                    for (int j = 0; j < columns; j++)
                    {
                        int sum = i * columns + j;
                        if (sum < countSprites)
                        {
                            GUIStyle tempStyle = buttonStyle;
                            tempStyle.margin = new RectOffset(0, 0, 0, 0);
                            if (spriteGroup.spriteGroup[sum] == TileEditor_SpriteCollection.GetActiveSprite())
                            {
                                tempStyle.normal = GUI.skin.button.active;
                            }
                            else
                            {
                                tempStyle.normal.background = null;
                            }
                            if (GUILayout.Button(spriteGroup.spriteGroup[i * columns + j].texture, tempStyle, GUILayout.Width(buttonWidth), GUILayout.Height(buttonWidth)))
                            {
                                TileEditor_SpriteCollection.ChangeActiveSprite(sum);
                            }
                        }                          
                    }
                    EditorGUILayout.EndHorizontal();
                }
                EditorGUILayout.EndScrollView();
                boxRect = GUILayoutUtility.GetLastRect(); ;
                EditorGUILayout.EndVertical();
            }
            else
            {
                boxRect = GUILayoutUtility.GetRect(Screen.width, 100);
                GUILayout.BeginArea(boxRect);
               GUILayout.EndArea();
            }
            
            GUILayout.Space(5);

            if (GUILayout.Button("Import Spridesheet"))
            {
                //TileEditor_SpriteCollection.ImportSpritesheet();
                TileEditor_SpritesheetWindow window = (TileEditor_SpritesheetWindow)EditorWindow.GetWindow(typeof(TileEditor_SpritesheetWindow));
                window.Init();
            }

            //Check for drag and drop of sprites
            bool showBox = false;
            if (countSprites == 0) showBox = true;
            DropAreaGUI(boxRect, showBox);
        }


        //Detect changes on GUI
        if (GUI.changed)
        {
            //Change the sprite group
            TileEditor_SpriteCollection.ChangeActiveSpriteGroup(TileEditor_SpriteCollection.activeGroupIndex);
        }

        
    }


    //Open a window to add a new spriteGroup
    public static void OpenNewGroupWindow()
    {
        TileEditor_AddGroupWindow groupWindow = (TileEditor_AddGroupWindow)EditorWindow.GetWindow(typeof(TileEditor_AddGroupWindow));
        groupWindow.Init();
    }



    //--------DRAG AND DROP------------------------------------------------------------------------------

    public void DropAreaGUI(Rect area, bool showBox)
    {
        Event evt = Event.current;
        Rect dropArea = area;
        if(showBox)GUI.Box(dropArea, "Drag and Drop sprites in here.");
        //GUILayout.Box("Drag and Drop sprites in here.", GUILayout.ExpandHeight(true), GUILayout.ExpandWidth(true));

        switch (evt.type)
        {
            case EventType.DragUpdated:
            case EventType.DragPerform:
                if (!dropArea.Contains(evt.mousePosition))
                    return;

                DragAndDrop.visualMode = DragAndDropVisualMode.Copy;

                if (evt.type == EventType.DragPerform)
                {
                    DragAndDrop.AcceptDrag();

                    foreach (Object draggedObject in DragAndDrop.objectReferences)
                    {
                        // Do On Drag Stuff here
                        Debug.Log(draggedObject.name);
                        string path = AssetDatabase.GetAssetPath(draggedObject);
                        if(AssetDatabase.LoadAssetAtPath(path, typeof(Sprite)))
                        {
                            TextureImporter ti = (TextureImporter)TextureImporter.GetAtPath(path);
                            ti.isReadable = true;
                            AssetDatabase.ImportAsset(path);
                            Sprite sprite = AssetDatabase.LoadAssetAtPath(path, typeof(Sprite)) as Sprite;
                            TileEditor_SpriteCollection.GetActiveSpriteGroup().AddSprite(sprite);
                            TileEditor_SpriteCollection.Save();
                            ti.isReadable = false;
                            AssetDatabase.ImportAsset(path);
                            TileEditor_SpriteCollection.Load();
                        }
                    }
                }
                break;
        }
    }
}
