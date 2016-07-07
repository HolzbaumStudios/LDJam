using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using System.Linq;
using System;

[System.Serializable]
public static class TileEditor_SpriteCollection{

    [System.NonSerialized]
    private static Sprite activeSprite;
    [System.NonSerialized]
    public static List<Sprite> spriteList = new List<Sprite>();

    [SerializeField]
    public static List<byte[]> byteList;


    public static Sprite GetActiveSprite()
    {
        return activeSprite;
    }

    public static void AddSprite(Sprite sprite)
    {
        spriteList.Add(sprite);
    }

    public static void ChangeActiveSprite(int index)
    {
        activeSprite = spriteList[index];
        TileEditor_BrushCollection.ChangeActiveBrush(null);
    }

    public static void SetSpriteNull()
    {
        activeSprite = null;
    }


    public static void ImportSpritesheet(string path)
    {
        Debug.Log("Executed");
        //string path = EditorUtility.OpenFilePanel("Select a spritesheet", "", "png");
        //path = path.Replace(Application.dataPath, "Assets");
        Sprite[] sprites = AssetDatabase.LoadAllAssetRepresentationsAtPath(path).OfType<Sprite>().ToArray();

        
        foreach (Sprite sprite in sprites)
        {

            Sprite newSprite = Sprite.Create(sprite.texture, new Rect(0, 0, sprite.texture.width, sprite.texture.height), new Vector2(0.5f, 0.5f));

            spriteList.Add(newSprite); 
        }
    }

    //Saves the sprite collection
    public static void Save()
    {
        ConvertToPng();
        TileEditor_SaveLoad.SaveSpriteCollection();
    }

    //Load the sprite collection
    public static void Load()
    {
        TileEditor_SaveLoad.LoadSpriteCollection();
        if(byteList != null)ConvertToSprite();
    }


    //Convert sprite info to byte to make it serializable
    public static void ConvertToPng()
    {
        byteList = new List<byte[]>();
        for (int i = 0; i < spriteList.Count; i++)
        {
            byte[] imageBytes;
            if (spriteList[i] != null)
            {
                Texture2D texture = spriteList[i].texture;
                imageBytes = texture.EncodeToPNG();

            }
            else
            {
                imageBytes = new byte[0];
            }
            byteList.Add(imageBytes);
        }
    }


    //Convert bytes to sprite
    public static void ConvertToSprite()
    {
        spriteList = new List<Sprite>();
        for (int i = 0; i < byteList.Count; i++)
        {
            Texture2D texture = new Texture2D(2, 2);
            texture.LoadImage(byteList[i]);
            spriteList.Add(Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f), texture.width));

        }

    }
}
