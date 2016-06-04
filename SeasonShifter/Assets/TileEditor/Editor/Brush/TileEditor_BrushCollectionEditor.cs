using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(TileEditor_BrushCollection))]
public class TileEditor_BrushCollectionEditor : Editor {

    TileEditor_BrushCollection brushCollection;

    void OnEnable()
    {
        brushCollection = (TileEditor_BrushCollection)target;
    }

    public override void OnInspectorGUI()
    {
        GUILayout.Button("Test");
        foreach(TileEditor_Brush brush in brushCollection.brushContainer)
        {
            if(GUILayout.Button(brush.GetBrushName()))
            {

            }
        }
    }
}
