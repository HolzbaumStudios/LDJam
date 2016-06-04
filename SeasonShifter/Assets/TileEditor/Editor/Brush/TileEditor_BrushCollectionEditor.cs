using UnityEngine;
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
        

        Rect scrollview = GUILayoutUtility.GetRect(10,10,150,80);
        Rect viewRect = new Rect(0, 0, 200, 70);
        scrollPosition = GUI.BeginScrollView(scrollview,scrollPosition, viewRect);
        GUILayout.BeginHorizontal();
            for(int i = 0; i < brushCollection.brushContainer.Count; i++)
            {
                TileEditor_Brush brush = brushCollection.brushContainer[i];
                int x = (5 + 5 * i) + 50 * i;
                if (GUI.Button(new Rect(x,10,50,50),brush.thumbnail, buttonStyle))
                {

                }
                GUI.Label(new Rect(x, 60, 50, 30), brush.GetBrushName(), labelStyle);                
            }
        GUILayout.EndHorizontal();
        GUI.EndScrollView();
        
    }
}
