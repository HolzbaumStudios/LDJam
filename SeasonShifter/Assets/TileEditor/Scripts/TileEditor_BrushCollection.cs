using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TileEditor_BrushCollection : MonoBehaviour {

    TileEditor_Brush activeBrush;
    public List<TileEditor_Brush> brushContainer; // A container for all brushes

    public TileEditor_BrushCollection()
    {
        brushContainer = new List<TileEditor_Brush>();
    }

    public TileEditor_Brush AddBrush(TileEditor_Brush brush)
    {
        brushContainer.Add(brush);
        return brush;
    }

    public void RemoveBrush(TileEditor_Brush brush)
    {
        brushContainer.Remove(brush);
    }    
   
}
