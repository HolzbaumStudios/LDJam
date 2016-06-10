﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class TileEditor_Brush {

    [System.NonSerialized]
    public Sprite[] sprites; //0 -> topLeft, 8 -> bottom right (9x9); 9-12 -> inner edge tiles; 13-15 -> horizontal line; 16-18 vertical lign
    [HideInInspector]
    public int maxNumberOfSprites = 19;
    [System.NonSerialized]
    public Texture2D thumbnail; //Brush image

    public string brushName;

    [SerializeField]
    private List<byte[]> byteList;
    [SerializeField]
    private byte[] thumbnailByte;


    public TileEditor_Brush()
    {
        brushName = "brush";
        if(sprites == null)sprites = new Sprite[maxNumberOfSprites];
        
    }

    public string GetBrushName()
    {
        return brushName;
    }

    //Convert sprite info to byte to make it serializable
    public void ConvertToPng()
    {
        byteList = new List<byte[]>();
        for (int i=0; i < maxNumberOfSprites; i++)
        {
            byte[] imageBytes;
            if (sprites[i] != null)
            {
                imageBytes = sprites[i].texture.EncodeToPNG();
                
            }
            else
            {
                imageBytes = new byte[0];
            }
            byteList.Add(imageBytes);
        }
        if (thumbnail != null)
        {
            thumbnailByte = thumbnail.EncodeToPNG();
        }
        else
        {
            thumbnailByte = new byte[0];
        }
    }

    //Convert bytes to sprite
    public void ConvertToSprite()
    {
        sprites = new Sprite[maxNumberOfSprites];
        for(int i=0; i < maxNumberOfSprites; i++)
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
