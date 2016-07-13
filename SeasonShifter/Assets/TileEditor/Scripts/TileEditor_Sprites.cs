using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class TileEditor_Sprites {

    [System.NonSerialized]
    public List<Sprite> spriteGroup;

    [SerializeField]
    private string spriteGroupName;

    [SerializeField]
    private List<byte[]> byteList;


    public TileEditor_Sprites(string name)
    {
        if(spriteGroup== null) spriteGroup = new List<Sprite>();
        SetGroupName(name);
        
    }

    public string GetGrouphName()
    {
        return spriteGroupName;
    }

    public void SetGroupName(string name)
    {
        spriteGroupName = name;
    }

    public void AddSprite(Sprite sprite)
    {
        spriteGroup.Add(sprite);
    }

    public void RemoveSprite(Sprite sprite)
    {
        spriteGroup.Remove(sprite);
    }


    //Convert sprite info to byte to make it serializable
    public void ConvertToPng()
    {
        byteList = new List<byte[]>();
        for (int i = 0; i < spriteGroup.Count; i++)
        {
            byte[] imageBytes;
            if (spriteGroup[i] != null)
            {
                Texture2D texture = spriteGroup[i].texture;
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
    public void ConvertToSprite()
    {
        spriteGroup = new List<Sprite>();
        for (int i = 0; i < byteList.Count; i++)
        {
            Texture2D texture = new Texture2D(2, 2);
            texture.LoadImage(byteList[i]);
            spriteGroup.Add(Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f), texture.width));

        }
    }
}
