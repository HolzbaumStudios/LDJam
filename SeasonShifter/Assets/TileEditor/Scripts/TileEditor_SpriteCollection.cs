using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class TileEditor_SpriteCollection{

    private static Sprite activeSprite;
    private static List<Sprite> spriteList;

    public static Sprite GetActiveSprite()
    {
        return activeSprite;
    }

    public static void AddSprite(Sprite sprite)
    {
        spriteList.Add(sprite);
    }

    public static void OpenImportWindow()
    {

    }
}
