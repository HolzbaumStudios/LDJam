using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using System.Linq;
using System;

public static class TileEditor_SpriteCollection{

    private static Sprite activeSprite;
    public static List<Sprite> spriteList = new List<Sprite>();


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
}
