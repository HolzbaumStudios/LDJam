using UnityEngine;
using System.Collections;

public class TileEditor_Brush {

    public Sprite[] sprites; //0 -> topLeft, 8 -> bottom right (9x9); 9-11 -> horizontal line; 12-14 vertical lign
    public Sprite thumbnail; //Brush image
    public string brushName;

    public TileEditor_Brush()
    {
        brushName = "brush";
        sprites = new Sprite[15];
    }
}
