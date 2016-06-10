using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class TileEditor_Brush {

    [System.NonSerialized]
    public Sprite[] sprites; //0 -> topLeft, 8 -> bottom right (9x9); 9-12 -> inner edge tiles; 13-15 -> horizontal line; 16-18 vertical lign
    [System.NonSerialized]
    public Texture2D thumbnail; //Brush image

    public string brushName;

    [SerializeField]
    private List<byte[]> byteList = new List<byte[]>();
    [SerializeField]
    private byte[] thumbnailByte;

    public TileEditor_Brush()
    {
        brushName = "brush";
        sprites = new Sprite[19];
    }

    public string GetBrushName()
    {
        return brushName;
    }

    //Convert sprite info to byte to make it serializable
    public void ConvertToPng()
    {
        for(int i=0; i < sprites.Length; i++)
        {
            if (sprites[i] != null)
            {
                byte[] imageBytes = sprites[i].texture.EncodeToPNG();
                byteList.Add(imageBytes);
            }
            
        }
        thumbnailByte = thumbnail.EncodeToPNG();
    }

    //Convert bytes to sprite
    public void ConvertToSprite()
    {
        int byteListCount = byteList.Count;
        sprites = new Sprite[byteListCount];
        for(int i=0; i < byteListCount; i++)
        {
            Texture2D texture = new Texture2D(2,2);
            texture.LoadImage(byteList[i]);
            sprites[i] = Sprite.Create(texture, new Rect(0,0,texture.width,texture.height), new Vector2(0.5f,0.5f), texture.width);
            
        }

        Texture2D thumbnailTexture = new Texture2D(2, 2);
        thumbnailTexture.LoadImage(thumbnailByte);
        thumbnail = thumbnailTexture;
    }
}
