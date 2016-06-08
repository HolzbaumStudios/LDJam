using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

[System.Serializable, InitializeOnLoad]
public static class TileEditor_BrushCollection {

    [SerializeField]
    private static TileEditor_Brush activeBrush;
    public static List<TileEditor_Brush> brushContainer =  new List<TileEditor_Brush>(); // A container for all brushes

    static TileEditor_BrushCollection()
    {
        LoadBrushCollection();
    }
    

    public static TileEditor_Brush AddBrush(TileEditor_Brush brush)
    {
        brushContainer.Add(brush);
        return brush;
    }

    public static void RemoveBrush(TileEditor_Brush brush)
    {
        brushContainer.Remove(brush);
    }
    
    public static void ChangeActiveBrush(TileEditor_Brush brush)
    {
        activeBrush = brush;
    }
    
    public static TileEditor_Brush GetActiveBrush()
    {
        return activeBrush;
    }
    
    public static void SaveBrushCollection()
    {
        TileEditor_SaveLoad.Save();
    }

    public static void LoadBrushCollection()
    {
        TileEditor_SaveLoad.LoadBrushCollection();
    }
   
}
