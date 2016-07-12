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
    private static TileEditor_Sprites activeSpriteGroup;

    public static int activeGroupIndex = 0;
    [System.NonSerialized]
    public static List<TileEditor_Sprites> spriteGroupCollection = new List<TileEditor_Sprites>();
    
    public static Sprite GetActiveSprite()
    {
        return activeSprite;
    }

    public static TileEditor_Sprites GetActiveSpriteGroup()
    {
        return activeSpriteGroup;
    }

    public static void AddGroup(TileEditor_Sprites spriteGroup)
    {
        spriteGroupCollection.Add(spriteGroup);
    }

    public static void ChangeActiveSprite(int index)
    {
        activeSprite = spriteGroupCollection[activeGroupIndex].spriteGroup[index];
        TileEditor_BrushCollection.ChangeActiveBrush(null);
    }

    public static void ChangeActiveSpriteGroup(int index)
    {
        if (spriteGroupCollection.Count > 0)
        {
            activeSpriteGroup = spriteGroupCollection[index];
        }
        else
        {
            activeSpriteGroup = null;
        }
    }

    public static void SetSpriteNull()
    {
        activeSprite = null;
    }


    public static void ImportSpritesheet(string path)
    {
        //string path = EditorUtility.OpenFilePanel("Select a spritesheet", "", "png");
        //path = path.Replace(Application.dataPath, "Assets");
        Sprite[] sprites = AssetDatabase.LoadAllAssetRepresentationsAtPath(path).OfType<Sprite>().ToArray();

        
        foreach (Sprite sprite in sprites)
        {

            Sprite newSprite = Sprite.Create(sprite.texture, new Rect(0, 0, sprite.texture.width, sprite.texture.height), new Vector2(0.5f, 0.5f));

            spriteGroupCollection[activeGroupIndex].spriteGroup.Add(newSprite); 
        }
    }

    //Saves the sprite collection
    public static void Save()
    {
        foreach (TileEditor_Sprites spriteGroup in TileEditor_SpriteCollection.spriteGroupCollection)
        {
            spriteGroup.ConvertToPng();
        }
        TileEditor_SaveLoad.SaveSpriteCollection();
    }

    //Load the sprite collection
    public static void Load()
    {
        TileEditor_SaveLoad.LoadSpriteCollection();
        foreach (TileEditor_Sprites spriteGroup in TileEditor_SpriteCollection.spriteGroupCollection)
        {
            spriteGroup.ConvertToSprite();
        }
    }

    public static string[] GetGroupNames()
    {

        List<string> groupNames = new List<string>();
        foreach(TileEditor_Sprites group in spriteGroupCollection)
        {
            groupNames.Add(group.GetGrouphName());
        }
        return groupNames.ToArray();
    }



    public static void AddGroup(string name)
    {
        spriteGroupCollection.Add(new TileEditor_Sprites(name));
        activeSpriteGroup = spriteGroupCollection.Last();
        activeGroupIndex = spriteGroupCollection.Count - 1;
        Save();
    }

    public static void DeleteGroup()
    {
        spriteGroupCollection.Remove(activeSpriteGroup);
        activeSpriteGroup = null;
        activeGroupIndex = 0;
        Save();
    }

}
