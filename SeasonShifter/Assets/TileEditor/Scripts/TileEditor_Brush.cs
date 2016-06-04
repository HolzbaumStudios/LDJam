using UnityEngine;
using System.Collections;

public class TileEditor_Brush {

    public Sprite[] sprites; //0 -> topLeft, 8 -> bottom right (9x9); 9-12 -> inner edge tiles; 13-15 -> horizontal line; 16-18 vertical lign
    public Texture thumbnail; //Brush image
    public string brushName;

    public TileEditor_Brush()
    {
        brushName = "brush";
        sprites = new Sprite[19];
    }

    public string GetBrushName()
    {
        return brushName;
    }
}
