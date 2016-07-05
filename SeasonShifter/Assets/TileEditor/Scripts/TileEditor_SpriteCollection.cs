using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using System.Linq;

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


    public static void ImportSpritesheet()
    {
        Debug.Log("Executed");
        string path = EditorUtility.OpenFilePanel("Select a spritesheet", "", "png");
        path = path.Replace(Application.dataPath, "Assets");
        Sprite[] sprites = AssetDatabase.LoadAllAssetsAtPath(path).OfType<Sprite>().ToArray();

        

        foreach (Sprite sprite in sprites)
        {
            Debug.Log("Import sprite: " + sprite.name);
            AddSprite(sprite);
        }
    }
}
