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

        GUILayout.Space(10);

        if(GUILayout.Button("Import Spridesheet"))
        {
            //TileEditor_SpriteCollection.ImportSpritesheet();
           TileEditor_SpritesheetWindow window = (TileEditor_SpritesheetWindow)EditorWindow.GetWindow(typeof(TileEditor_SpritesheetWindow));            
            window.Init();
        }

        TileEditor_SpriteCollection.activeGroupIndex = EditorGUILayout.Popup(TileEditor_SpriteCollection.activeGroupIndex, new string[] { "Hallo" });
        int index = TileEditor_SpriteCollection.activeGroupIndex;
        
        int countSprites = TileEditor_SpriteCollection.spriteGroupCollection[index].spriteGroup.Count;
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
                        if (TileEditor_SpriteCollection.spriteGroupCollection[index].spriteGroup[sum] == TileEditor_SpriteCollection.GetActiveSprite())
                        {
                            tempStyle.normal = GUI.skin.button.active;
                        }
                        else
                        {
                            tempStyle.normal.background = null;
                        }
                        if (GUILayout.Button(TileEditor_SpriteCollection.spriteGroupCollection[index].spriteGroup[i * columns + j].texture, tempStyle, GUILayout.Width(buttonWidth), GUILayout.Height(buttonWidth)))
                        {
                            TileEditor_SpriteCollection.ChangeActiveSprite(sum);
                        }
                    }                          
                }
                EditorGUILayout.EndHorizontal();
            }
            EditorGUILayout.EndScrollView();
            EditorGUILayout.EndVertical();
        }
        GUILayout.Space(5);  
        
    }
}
