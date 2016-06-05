﻿using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(TileEditor_BrushCollection))]
public class TileEditor_BrushCollectionEditor : Editor {

    TileEditor_BrushCollection brushCollection;
    Vector2 scrollPosition = Vector2.zero;
    GUIStyle labelStyle;
    GUIStyle buttonStyle;

    void OnEnable()
    {
        brushCollection = (TileEditor_BrushCollection)target;
    }


    public override void OnInspectorGUI()
    {
        //Define style
        labelStyle = new GUIStyle(GUI.skin.label);
        labelStyle.alignment = TextAnchor.MiddleCenter;
        buttonStyle = new GUIStyle(GUI.skin.button);
        buttonStyle.normal.background = null;

        int brushCount = brushCollection.brushContainer.Count;
        Rect scrollview = GUILayoutUtility.GetRect(10,10,150,70);
        Rect viewRect = new Rect(0, 0, 60 * brushCount, 70);
        scrollPosition = GUI.BeginScrollView(scrollview,scrollPosition, viewRect);
        GUILayout.BeginHorizontal();
            for(int i = 0; i < brushCount; i++)
            {
                TileEditor_Brush brush = brushCollection.brushContainer[i];
                int x = (5 + 5 * i) + 50 * i;
                GUIStyle tempStyle = buttonStyle;
                if (brush == brushCollection.GetActiveBrush())
                {
                    tempStyle.normal = GUI.skin.button.active;
                }
                else
                {
                    tempStyle.normal.background = null;
                }
                if (GUI.Button(new Rect(x,10,50,50),brush.thumbnail, tempStyle))
                {
                    brushCollection.ChangeActiveBrush(brush);
                }
                GUI.Label(new Rect(x, 60, 50, 20), brush.GetBrushName(), labelStyle);                
            }
        GUILayout.EndHorizontal();
        GUI.EndScrollView();

        //Buttons
        GUILayout.BeginHorizontal();
            //Opens window to add brush
            if (GUILayout.Button("+ Brush", GUILayout.Width(80)))
            {
                TileEditor_BrushWindow window = (TileEditor_BrushWindow)EditorWindow.GetWindow(typeof(TileEditor_BrushWindow));
                window.Init();
                window.AddBrush();
            }

            //if a brush is selected add buttons to delete and modify
            if(brushCollection.GetActiveBrush() != null)
            {
            Debug.Log(brushCollection.GetActiveBrush());
                if (GUILayout.Button("Delete", GUILayout.Width(80)))
                {
                    brushCollection.RemoveBrush(brushCollection.GetActiveBrush());
                }

                if (GUILayout.Button("Modify", GUILayout.Width(80)))
                {
                    TileEditor_BrushWindow window = (TileEditor_BrushWindow)EditorWindow.GetWindow(typeof(TileEditor_BrushWindow));
                    window.Init();
                    window.GetBrush();
                }
            }
        GUILayout.EndHorizontal();
        GUILayout.Space(20);

    }
}
