﻿using UnityEngine;
using System.Collections;

public class TileEditor_ObjectHandler : MonoBehaviour {

    GameObject[,] spriteArray = new GameObject[50, 50];


    void Start()
    {
  
/*        for(int x = 0; x < 50; x++)
        {
            for (int y = 0; y < 50; y++)
            {
                spriteArray[x, y] = null;
            }
        }*/
    }

    public Sprite[] testSprite;

    public void AddSprite(Vector3 position)
    {
        //Get array value by rounding down position
        int x = (int)position.x;
        int y = (int)position.y;
        //If there is no object, create it now
        if (spriteArray[x, y] == null)
        {
            spriteArray[x, y] = new GameObject("Tile");
            spriteArray[x, y].transform.position = position;
            spriteArray[x, y].transform.SetParent(this.transform);
            spriteArray[x, y].AddComponent<SpriteRenderer>();
        }

        //Change sprite of the selected field
        ChangeSprite(x, y);
        CheckSurroundingTiles(x, y);
    }   

    void CheckSurroundingTiles(int x, int y)
    {
        //Check the surrounding tiles and change them if necessary
        //Middle line
        if (spriteArray[x + 1, y] != null) ChangeSprite(x + 1, y);
        if (spriteArray[x - 1, y] != null) ChangeSprite(x - 1, y);
        //Top line
        if (spriteArray[x + 1, y + 1] != null) ChangeSprite(x + 1, y + 1);
        if (spriteArray[x, y + 1] != null) ChangeSprite(x, y + 1);
        if (spriteArray[x - 1, y + 1] != null) ChangeSprite(x - 1, y + 1);
        //Bottom line
        if (spriteArray[x + 1, y - 1] != null) ChangeSprite(x + 1, y - 1);
        if (spriteArray[x, y - 1] != null) ChangeSprite(x, y - 1);
        if (spriteArray[x - 1, y - 1] != null) ChangeSprite(x - 1, y - 1);
    }

    void ChangeSprite(int x, int y)
    {
        //Check which sprite has to be added
        SpriteRenderer tileImage = spriteArray[x, y].GetComponent<SpriteRenderer>();
        if (spriteArray[x, y + 1] == null)
        {
            //Tile is a top tile
            if (spriteArray[x - 1, y] == null)
            {
                tileImage.sprite = testSprite[0];
            }
            else if (spriteArray[x + 1, y] == null)
            {
                tileImage.sprite = testSprite[2];
            }
            else
            {
                tileImage.sprite = testSprite[1];
            }
        }
        else if (spriteArray[x, y - 1] == null)
        {
            //Tile is a bottom tile
            if (spriteArray[x - 1, y] == null)
            {
                tileImage.sprite = testSprite[6];
            }
            else if (spriteArray[x + 1, y] == null)
            {
                tileImage.sprite = testSprite[8];
            }
            else
            {
                tileImage.sprite = testSprite[7];
            }
        }
        else
        {
            //Tile is a middle tile
            if (spriteArray[x - 1, y] == null)
            {
                tileImage.sprite = testSprite[3];
            }
            else if (spriteArray[x + 1, y] == null)
            {
                tileImage.sprite = testSprite[5];
            }
            else
            {
                tileImage.sprite = testSprite[4];
            }
        }
    }

    public void RemoveSprite(Vector3 position)
    {
        //Get array value by rounding down position
        int x = (int)position.x;
        int y = (int)position.y;
        //If there is a object, delete it now
        if (spriteArray[x, y] != null)
        {
            Destroy(spriteArray[x, y]);
            CheckSurroundingTiles(x, y);
        }
    }

}
