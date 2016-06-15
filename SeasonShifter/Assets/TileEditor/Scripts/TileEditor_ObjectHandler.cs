using UnityEngine;
using System.Collections;

[System.Serializable, ExecuteInEditMode]
public class TileEditor_ObjectHandler : MonoBehaviour {

    private GameObject[,] spriteArray;
    [SerializeField, HideInInspector]
    private int columns;
    [SerializeField, HideInInspector]
    private int rows;

    //TileEditor_BrushCollection brushCollection;
    TileEditor_Brush brush;

    public void CreateArray(int x, int y)
    {
        spriteArray = new GameObject[x, y];
        columns = x;
        rows = y;
    }

    public void CheckIfArrayExists()
    {
        if(spriteArray == null )
        {
            spriteArray = new GameObject[columns, rows];
        }
    }

    public void AddSprite(Vector3 position)
    {
        //Get active brush
        //brushCollection = this.gameObject.GetComponent<TileEditor_BrushCollection>();
        brush = TileEditor_BrushCollection.GetActiveBrush();
        if (brush != null)
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
        else
        {
            Debug.LogWarning("You have to select a brush first!");
        }
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
        string tileName;
        if(spriteArray[x, y+1] == null && spriteArray[x, y-1] == null) //if top and bottom tile null
        {
            if (spriteArray[x -1, y] == null) //if right top and right bottom tile are free set a single horizontal sprite
            {
                tileImage.sprite = brush.sprites[13];
                tileName = "Tile_Id13";
            }
            else if (spriteArray[x + 1, y] == null) //if right top and right bottom tile are free set a single horizontal sprite
            {
                tileImage.sprite = brush.sprites[15];
                tileName = "Tile_Id15";
            }
            else
            {
                tileImage.sprite = brush.sprites[14];
                tileName = "Tile_Id14";
            }
        }
        else if (spriteArray[x, y + 1] == null)
        {
            if(spriteArray[x - 1, y] == null && spriteArray[x + 1, y] == null) //Tile is a top tile and has no side tiles
            {
                if (brush.sprites[16] != null)
                {
                    tileImage.sprite = brush.sprites[16];
                    tileName = "Tile_Id16";
                }
                else
                {
                    tileImage.sprite = brush.sprites[4];
                    tileName = "Tile_Id4";
                }
            }
            else if (spriteArray[x - 1, y] == null) //tile is a top tile and has no left tile
            {
                tileImage.sprite = brush.sprites[0];
                tileName = "Tile_Id0";
            }
            else if (spriteArray[x + 1, y] == null) //tile is a top tile and has no right tile
            {
                tileImage.sprite = brush.sprites[2];
                tileName = "Tile_Id2";
            }
            else
            {
                tileImage.sprite = brush.sprites[1]; //tile is a top tile and has tiles on both sides
                tileName = "Tile_Id1";
            }
        }
        else if (spriteArray[x, y - 1] == null)
        {
            
            if(spriteArray[x - 1, y] == null && spriteArray[x + 1, y] == null) //Tile is a bottom tile and has no left or right tile
            {
                tileImage.sprite = brush.sprites[18];
                tileName = "Tile_Id18";
            }
            else if (spriteArray[x - 1, y] == null)  //Tile is a bottom tile and has no right tile
            {
                tileImage.sprite = brush.sprites[6];
                tileName = "Tile_Id6";
            }
            else if (spriteArray[x + 1, y] == null) //Tile is a bottom tile and has no left tile
            {
                tileImage.sprite = brush.sprites[8];
                tileName = "Tile_Id8";
            }
            else //Tile is a bottom tile and has tiles on both sides
            {
                tileImage.sprite = brush.sprites[7];
                tileName = "Tile_Id7";
            }
        }
        else
        {
            //Tile is a middle tile
            if(spriteArray[x - 1, y] == null && spriteArray[x + 1, y] == null)
            {
                tileImage.sprite = brush.sprites[17];
                tileName = "Tile_Id17";
            }
            else if (spriteArray[x - 1, y] == null)
            {
                tileImage.sprite = brush.sprites[3]; //else set a normal side tile
                tileName = "Tile_Id3";
            }
            else if (spriteArray[x + 1, y] == null)
            {
                tileImage.sprite = brush.sprites[5];
                tileName = "Tile_Id5";
            }
            else //The center tiles
            {
                //Check for edges
                if (spriteArray[x - 1, y + 1] == null) //top left inner edge
                {
                    tileImage.sprite = brush.sprites[9];
                    tileName = "Tile_Id9";
                }
                else if (spriteArray[x + 1, y + 1] == null) //top right inner edge
                {
                    tileImage.sprite = brush.sprites[10];
                    tileName = "Tile_Id10";
                }
                else if (spriteArray[x - 1, y - 1] == null) //bottom left inner edge
                {
                    tileImage.sprite = brush.sprites[11];
                    tileName = "Tile_Id11";
                }
                else if (spriteArray[x + 1, y - 1] == null) //bottom right inner edge
                {
                    tileImage.sprite = brush.sprites[12];
                    tileName = "Tile_Id12";
                }
                else //normal center tile
                {
                    tileImage.sprite = brush.sprites[4];
                    tileName = "Tile_Id4";
                }
            }
        }

        spriteArray[x, y].name = tileName;
    }

    public void RemoveSprite(Vector3 position)
    {
        //Get array value by rounding down position
        int x = (int)position.x;
        int y = (int)position.y;
        //If there is a object, delete it now
        if (spriteArray[x, y] != null)
        {
            DestroyImmediate(spriteArray[x, y]);
            if(TileEditor_BrushCollection.GetActiveBrush() != null)CheckSurroundingTiles(x, y);
        }
    }

    //Creates new array and loads all the tiles
    public void LoadTiles()
    {
        spriteArray = new GameObject[columns,rows];
        foreach(Transform child in this.transform)
        {
            int x = (int)child.position.x;
            int y = (int)child.position.y;
            spriteArray[x, y] = child.gameObject;
        }
    }

    //Creates a collider based on the child objects
    public void CreateCollider()
    {
        PolygonCollider2D collider = this.gameObject.AddComponent<PolygonCollider2D>();
        //Determine path points
        Vector2[] pathPoints;
        for(int y=0; y < rows; y++)
        {
            for(int x=0; x < columns; x++)
            {
                if(spriteArray[x,y] != null)
                {
                   
                }
            }
        }
    }


}
