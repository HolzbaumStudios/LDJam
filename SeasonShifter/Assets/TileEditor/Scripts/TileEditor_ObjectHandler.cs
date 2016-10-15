using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class TileEditor_ObjectHandler : MonoBehaviour {

    private GameObject[,,] spriteArray;

    [SerializeField, HideInInspector]
    private int columns;
    [SerializeField, HideInInspector]
    private int rows;

    List<GameObject> tileList; //User for the fill function

    //TileEditor_BrushCollection brushCollection;
    TileEditor_Brush brush;

    string activeSortingLayer = "Default";
    int layerCount;
    int orderInLayer = 0;
    Material activeMaterial;
    bool flipX = false, flipY = false;
    bool slope = false; //Set true to draw diagonal tiles
    //Collider variables
    int colliderType = 0;
    bool isTrigger = false;
    private Vector2 boxColliderSize = new Vector2(1, 1);
    private Vector2 boxColliderOffset = new Vector2(0, 0);

    public void CreateArray(int x, int y)
    {
        layerCount = SortingLayer.layers.Length;
        spriteArray = new GameObject[x, y, layerCount];
        columns = x;
        rows = y;
    }

    public void SetSortingLayer(string layerName, int sortingOrder)
    {
        activeSortingLayer = layerName;
        orderInLayer = sortingOrder;
    }

    public void SetMaterial(Material material)
    {
        activeMaterial = material;
    }

    public void SetFlipInformation(bool x, bool y)
    {
        flipX = x; flipY = y;
    }

    public void SetColliderInformation(int index, bool isTrigger, Vector2 size, Vector2 offset)
    {
        colliderType = index;
        this.isTrigger = isTrigger;
        boxColliderSize = size;
        boxColliderOffset = offset;
    }

    public void SetSlope(bool slope) //Set if diagonal tiles should be used or not
    {
        this.slope = slope;
    }

    public void CheckIfArrayExists()
    {
        if(spriteArray == null )
        {
            spriteArray = new GameObject[columns, rows, layerCount];
        }
    }

    public void AddSprite(Vector3 position, bool checkSurrounding)
    {
        //Get active brush
        //brushCollection = this.gameObject.GetComponent<TileEditor_BrushCollection>();
        bool singleSprite = false;
        if(TileEditor_BrushCollection.GetActiveBrush() != null)
        {
            brush = TileEditor_BrushCollection.GetActiveBrush();
        }
        else
        {
            singleSprite = true;
        }
        
        if (brush != null || TileEditor_SpriteCollection.GetActiveSprite() != null)
        {
            //Get array value by rounding down position
            int x = (int)position.x;
            int y = (int)position.y;
            int layerId = SortingLayer.GetLayerValueFromName(activeSortingLayer);
            Debug.Log("Layer ID: " + layerId);
            //If there is no object, create it now
            if (spriteArray[x, y, layerId] == null)
            {
                GameObject layerContainer = this.transform.FindChild(activeSortingLayer).gameObject;
                if (layerContainer != null)
                {
                    spriteArray[x, y, layerId] = new GameObject("Object"); //If it is a tile, the name is changed in the ChangeSprite function
                    spriteArray[x, y, layerId].transform.position = position;
                    spriteArray[x, y, layerId].transform.SetParent(layerContainer.transform);
                    spriteArray[x, y, layerId].AddComponent<SpriteRenderer>();
                }
                else
                {
                    Debug.LogError("A corresponding layer container is missing!");
                    return;
                }
            }

            //Change sprite of the selected field
            if (!singleSprite)
            {
                ChangeSprite(x, y, layerId);
                if(checkSurrounding)CheckSurroundingTiles(x, y, layerId);
            }
            else
            {
                ChangeSingleSprite(x, y, layerId);
            }
        }
        else
        {
            Debug.LogWarning("You have to select a brush first!");
        }
    }


    void CheckSurroundingTiles(int x, int y, int layer)
    {
        //Check the surrounding tiles and change them if necessary
        //Middle line
        if (spriteArray[x + 1, y, layer] != null) ChangeSprite(x + 1, y, layer);
        if (spriteArray[x - 1, y, layer] != null) ChangeSprite(x - 1, y, layer);
        //Top line
        if (spriteArray[x + 1, y + 1, layer] != null) ChangeSprite(x + 1, y + 1, layer);
        if (spriteArray[x, y + 1, layer] != null) ChangeSprite(x, y + 1, layer);
        if (spriteArray[x - 1, y + 1, layer] != null) ChangeSprite(x - 1, y + 1, layer);
        //Bottom line
        if (spriteArray[x + 1, y - 1, layer] != null) ChangeSprite(x + 1, y - 1, layer);
        if (spriteArray[x, y - 1, layer] != null) ChangeSprite(x, y - 1, layer);
        if (spriteArray[x - 1, y - 1, layer] != null) ChangeSprite(x - 1, y - 1, layer);
    }

    void ChangeSprite(int x, int y, int layer)
    {
        //Check which sprite has to be added
        SpriteRenderer tileImage = spriteArray[x, y, layer].GetComponent<SpriteRenderer>();
        tileImage.sortingLayerName = activeSortingLayer; //Set the sorting layer of the sprite
        if(activeMaterial!=null)tileImage.material = activeMaterial;
        tileImage.sortingOrder = orderInLayer;
        tileImage.flipX = this.flipX;
        tileImage.flipY = this.flipY;
        string tileName;
        if(spriteArray[x, y+1, layer] == null && spriteArray[x, y-1, layer] == null) //if top and bottom tile null
        {
            if (spriteArray[x -1, y, layer] == null) //if right top and right bottom tile are free set a single horizontal sprite
            {
                tileImage.sprite = brush.sprites[13];
                tileName = "Tile_Id13";
            }
            else if (spriteArray[x + 1, y, layer] == null) //if right top and right bottom tile are free set a single horizontal sprite
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
        else if (spriteArray[x, y + 1, layer] == null)
        {
            if(spriteArray[x - 1, y, layer] == null && spriteArray[x + 1, y, layer] == null) //Tile is a top tile and has no side tiles
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
            else if (spriteArray[x - 1, y, layer] == null) //tile is a top tile and has no left tile  --- TOP LEFT TILE
            {
                if (!slope)
                {
                    tileImage.sprite = brush.sprites[0];
                    tileName = "Tile_Id0";
                }
                else
                {
                    tileImage.sprite = brush.sprites[19];
                    tileName = "Tile_Id19";
                }
            }
            else if (spriteArray[x + 1, y, layer] == null) //tile is a top tile and has no right tile  ---  TOP RIGHT TILE
            {
                if (!slope)
                {
                    tileImage.sprite = brush.sprites[2];
                    tileName = "Tile_Id2";
                }
                else
                {
                    tileImage.sprite = brush.sprites[20];
                    tileName = "Tile_Id20";
                }
            }
            else
            {
                tileImage.sprite = brush.sprites[1]; //tile is a top tile and has tiles on both sides
                tileName = "Tile_Id1";
            }
        }
        else if (spriteArray[x, y - 1, layer] == null)
        {
            
            if(spriteArray[x - 1, y, layer] == null && spriteArray[x + 1, y, layer] == null) //Tile is a bottom tile and has no left or right tile
            {
                tileImage.sprite = brush.sprites[18];
                tileName = "Tile_Id18";
            }
            else if (spriteArray[x - 1, y, layer] == null)  //Tile is a bottom tile and has no left tile  --- BOTTOM LEFT TILE
            {
                if (!slope)
                {
                    tileImage.sprite = brush.sprites[6];
                    tileName = "Tile_Id6";
                }
                else
                {
                    tileImage.sprite = brush.sprites[21];
                    tileName = "Tile_Id21";
                }
            }
            else if (spriteArray[x + 1, y, layer] == null) //Tile is a bottom tile and has no right tile  --- BOTTOM RIGHT TILE
            {
                if (!slope)
                {
                    tileImage.sprite = brush.sprites[8];
                    tileName = "Tile_Id8";
                }
                else
                {
                    tileImage.sprite = brush.sprites[22];
                    tileName = "Tile_Id22";
                }
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
            if(spriteArray[x - 1, y,layer] == null && spriteArray[x + 1, y,layer] == null)
            {
                tileImage.sprite = brush.sprites[17];
                tileName = "Tile_Id17";
            }
            else if (spriteArray[x - 1, y, layer] == null)
            {
                tileImage.sprite = brush.sprites[3]; //else set a normal side tile
                tileName = "Tile_Id3";
            }
            else if (spriteArray[x + 1, y, layer] == null)
            {
                tileImage.sprite = brush.sprites[5];
                tileName = "Tile_Id5";
            }
            else //The center tiles
            {
                //Check for edges
                if (spriteArray[x - 1, y + 1, layer] == null) //top left inner edge
                {
                    if (!slope)
                    {
                        tileImage.sprite = brush.sprites[9];
                        tileName = "Tile_Id9";
                    }
                    else
                    {
                        tileImage.sprite = brush.sprites[23];
                        tileName = "Tile_Id23";
                    }
                }
                else if (spriteArray[x + 1, y + 1, layer] == null) //top right inner edge
                {
                    if (!slope)
                    {
                        tileImage.sprite = brush.sprites[10];
                        tileName = "Tile_Id10";
                    }
                    else
                    {
                        tileImage.sprite = brush.sprites[24];
                        tileName = "Tile_Id24";
                    }
                }
                else if (spriteArray[x - 1, y - 1, layer] == null) //bottom left inner edge
                {
                    if (!slope)
                    {
                        tileImage.sprite = brush.sprites[11];
                        tileName = "Tile_Id11";
                    }
                    else
                    {
                        tileImage.sprite = brush.sprites[25];
                        tileName = "Tile_Id25";
                    }
                }
                else if (spriteArray[x + 1, y - 1, layer] == null) //bottom right inner edge
                {
                    if (!slope)
                    {
                        tileImage.sprite = brush.sprites[12];
                        tileName = "Tile_Id12";
                    }
                    else
                    {
                        tileImage.sprite = brush.sprites[26];
                        tileName = "Tile_Id26";
                    }
                }
                else //normal center tile
                {
                    tileImage.sprite = brush.sprites[4];
                    tileName = "Tile_Id4";
                }
            }
        }

        spriteArray[x, y, layer].name = tileName;
    }

    //if no brush is used
    public void ChangeSingleSprite(int x, int y, int layer)
    {
        SpriteRenderer sprite = spriteArray[x, y, layer].GetComponent<SpriteRenderer>();
        sprite.sortingLayerName = activeSortingLayer; //Set the sorting layer of the sprite
        if (activeMaterial != null) sprite.material = activeMaterial;
        sprite.sortingOrder = orderInLayer;
        sprite.flipX = this.flipX;
        sprite.flipY = this.flipY;

        sprite.sprite = TileEditor_SpriteCollection.GetActiveSprite();

        switch (colliderType)
        {
            case 1:
            {
                BoxCollider2D boxCollider = spriteArray[x, y, layer].AddComponent<BoxCollider2D>();
                if (isTrigger) boxCollider.isTrigger = true;
                boxCollider.size = boxColliderSize;
                boxCollider.offset = boxColliderOffset;
                break; 
            }
            case 2:
            {
                CircleCollider2D circleCollider = spriteArray[x, y, layer].AddComponent<CircleCollider2D>();
                if (isTrigger) circleCollider.isTrigger = true;
                break;
            }
            case 3:
            {
                PolygonCollider2D polygonCollider = spriteArray[x, y, layer].AddComponent<PolygonCollider2D>();
                if (isTrigger) polygonCollider.isTrigger = true;
                break;
            }
        }    
    }

    public void RemoveSprite(Vector3 position, bool checkSurrounding)
    {
        //Get array value by rounding down position
        int x = (int)position.x;
        int y = (int)position.y;
        int layerId = SortingLayer.GetLayerValueFromName(activeSortingLayer);
        //If there is a object, delete it now
        if (spriteArray[x, y, layerId] != null)
        {
            DestroyImmediate(spriteArray[x, y, layerId]);
            brush = TileEditor_BrushCollection.GetActiveBrush();
            if (brush != null && checkSurrounding)
            {
                CheckSurroundingTiles(x, y, layerId);
            }
        }
    }

    //Fills an area with the selctes brush
    public void FillArea(Vector3 positon)
    {
        tileList = new List<GameObject>();

        FillTiles(positon);
    }

    void FillTiles(Vector3 position)
    {
        int x = (int)position.x;
        int y = (int)position.y;
        int layerId = SortingLayer.GetLayerValueFromName(activeSortingLayer);

        AddSprite(position, true);
        tileList.Add(spriteArray[x,y, layerId]);


        GameObject tempTile;
        tempTile = spriteArray[x + 1, y, layerId];
        if (tempTile != null && !CheckIfTileInList(tempTile))
        {             
            FillTiles(new Vector3(x + 1, y));
        }
        tempTile = spriteArray[x - 1, y, layerId];
        if (tempTile != null && !CheckIfTileInList(tempTile))
        {
            FillTiles(new Vector3(x - 1, y));
        }
        tempTile = spriteArray[x, y + 1, layerId];
        if (tempTile != null && !CheckIfTileInList(tempTile))
        {
            FillTiles(new Vector3(x, y + 1));
        }
        tempTile = spriteArray[x, y - 1, layerId];
        if (tempTile != null && !CheckIfTileInList(tempTile))
        {
            FillTiles(new Vector3(x, y - 1));
        }
        
    }

    /// <summary>
    /// Selection of area
    /// </summary>
    public void SelectArea(Vector2 startingPoint, Vector2 endPoint, bool erase, bool checkSurrounding)
    {
        int x1, x2, y1, y2;
        if(startingPoint.x > endPoint.x)
        {
            x2 = (int)startingPoint.x;
            x1 = (int)endPoint.x;
        }
        else
        {
            x1 = (int)startingPoint.x;
            x2 = (int)endPoint.x;
        }
        if (startingPoint.y > endPoint.y)
        {
            y2 = (int)startingPoint.y;
            y1 = (int)endPoint.y;
        }
        else
        {
            y1 = (int)startingPoint.y;
            y2 = (int)endPoint.y;
        }

        for(int y = y1; y <= y2; y++)
        {
            for (int x = x1; x <= x2; x++)
            {
                if (!erase)
                {
                    AddSprite(new Vector3(x + 0.5f, y + 0.5f), checkSurrounding);
                }
                else
                {
                    RemoveSprite(new Vector3(x + 0.5f, y + 0.5f), checkSurrounding);
                }
            }
        }

    }


    bool CheckIfTileInList(GameObject tile)
    {
        foreach(GameObject tileObject in tileList)
        {
            if (tileObject == tile) return true;
        }
        return false;
    }

    //Creates new array and loads all the tiles
    public void LoadTiles()
    {
        spriteArray = new GameObject[columns,rows, layerCount];
        foreach(Transform child in this.transform)
        {
            foreach (Transform subchild in child)
            {
                int x = (int)subchild.position.x;
                int y = (int)subchild.position.y;
                int layerId = SortingLayer.GetLayerValueFromName(child.name);
                Debug.Log("Layer ID: " + layerId);
                spriteArray[x, y, layerId] = subchild.gameObject;
            }
        }
    }

    //Get the number of sorting layers
    public void CountSortingLayers()
    {
        layerCount = SortingLayer.layers.Length;
    }

    //Create layer containers if not present
    public void CreateLayerContainers()
    {
        var layers = SortingLayer.layers;

        foreach(var layer in layers)
        {
            Debug.Log("Layer Name: " + layer.name);
            if(!this.transform.FindChild(layer.name))
            {
                GameObject container = new GameObject(layer.name);
                container.transform.SetParent(this.gameObject.transform);
                if (layer.name == "Water")
                {
                    container.tag = "Water";
                    container.AddComponent<WaterMovement>();
                }
            }
        }
    }

    //Creates a collider based on the child objects
    public void CreateCollider(int layer = 0)
    {
        GameObject child = this.transform.FindChild(SortingLayer.layers[layer].name).gameObject;
        PolygonCollider2D collider;
        if (child.GetComponent<PolygonCollider2D>())
        {
            collider = child.GetComponent<PolygonCollider2D>();
        }
        else
        {
            collider = child.AddComponent<PolygonCollider2D>();
        }

        //Determine path points
        List<Vector2> pathPoints;
        List<Vector2[]> pathList = new List<Vector2[]>();
        List<GameObject> bottomLeftTiles = new List<GameObject>(); //To determine if these tiles have already been used
        bool finishedAllChecks = false;
        int topCounter = 0;

        while (!finishedAllChecks && topCounter < 500)
        {
            topCounter++;
            pathPoints = new List<Vector2>();
            bool edgeTileFound = false;
            GameObject startTile = null;
            for (int y = 0; y < rows; y++)
            {
                for (int x = 0; x < columns; x++)
                {
                    if (spriteArray[x, y, layer] != null && (spriteArray[x, y, layer].name == "Tile_Id6" || spriteArray[x, y, layer].name == "Tile_Id21") && !CheckIfVectorExists(spriteArray[x,y, layer],bottomLeftTiles))
                    {
                        Debug.Log("Tile_Id6 found");
                        edgeTileFound = true;
                        startTile = spriteArray[x, y, layer];
                        bottomLeftTiles.Add(startTile);
                        x = columns;
                        y = rows;
                    }
                }
            }


            int direction = 0; //0 = up, 1 = right, 2 = down, 3 =  left
            int counter = 0; //to get out of an endless loop
            if (startTile != null)
            {
                int xPosition = (int)startTile.transform.position.x;
                int yPosition = (int)startTile.transform.position.y;

                while (edgeTileFound && counter < 2000)
                {
                    counter++;
                    switch (direction) //Make a step in the set direction
                    {
                        case 0: yPosition++; break;
                        case 1: xPosition++; break;
                        case 2: yPosition--; break;
                        case 3: xPosition--; break;
                    }

                    //Debug.Log("X: " + xPosition + " , Y: " + yPosition);

                    switch (spriteArray[xPosition, yPosition, layer].name)
                    {
                        case "Tile_Id0": direction = 1; pathPoints.Add(spriteArray[xPosition, yPosition, layer].transform.position + new Vector3(-0.5f, 0.5f, 0)); break;
                        case "Tile_Id2": direction = 2; pathPoints.Add(spriteArray[xPosition, yPosition, layer].transform.position + new Vector3(0.5f, 0.5f, 0)); break;
                        case "Tile_Id6": direction = 0; pathPoints.Add(spriteArray[xPosition, yPosition, layer].transform.position + new Vector3(-0.5f, -0.5f, 0)); bottomLeftTiles.Add(spriteArray[xPosition,yPosition, layer]); break;
                        case "Tile_Id8": direction = 3; pathPoints.Add(spriteArray[xPosition, yPosition, layer].transform.position + new Vector3(0.5f, -0.5f, 0)); break;
                        case "Tile_Id9": direction = 0; pathPoints.Add(spriteArray[xPosition, yPosition, layer].transform.position + new Vector3(-0.5f, 0.5f, 0)); break;
                        case "Tile_Id10": direction = 1; pathPoints.Add(spriteArray[xPosition, yPosition, layer].transform.position + new Vector3(0.5f, 0.5f, 0)); break;
                        case "Tile_Id11": direction = 3; pathPoints.Add(spriteArray[xPosition, yPosition, layer].transform.position + new Vector3(-0.5f, -0.5f, 0)); break;
                        case "Tile_Id12": direction = 2; pathPoints.Add(spriteArray[xPosition, yPosition, layer].transform.position + new Vector3(0.5f, -0.5f, 0)); break;
                        case "Tile_Id19": direction = 1; pathPoints.Add(spriteArray[xPosition, yPosition, layer].transform.position + new Vector3(-0.5f, -0.5f, 0)); pathPoints.Add(spriteArray[xPosition, yPosition, layer].transform.position + new Vector3(0.5f, 0.5f, 0)); break;
                        case "Tile_Id20": direction = 2; pathPoints.Add(spriteArray[xPosition, yPosition, layer].transform.position + new Vector3(-0.5f, 0.5f, 0)); pathPoints.Add(spriteArray[xPosition, yPosition, layer].transform.position + new Vector3(0.5f, -0.5f, 0)); break;
                        case "Tile_Id21": direction = 0; pathPoints.Add(spriteArray[xPosition, yPosition, layer].transform.position + new Vector3(0.5f, -0.5f, 0)); pathPoints.Add(spriteArray[xPosition, yPosition, layer].transform.position + new Vector3(-0.5f, 0.5f, 0)); bottomLeftTiles.Add(spriteArray[xPosition, yPosition, layer]); break;
                        case "Tile_Id22": direction = 3; pathPoints.Add(spriteArray[xPosition, yPosition, layer].transform.position + new Vector3(0.5f, 0.5f, 0)); pathPoints.Add(spriteArray[xPosition, yPosition, layer].transform.position + new Vector3(-0.5f, -0.5f, 0)); break;
                        case "Tile_Id23": direction = 0; break;
                        case "Tile_Id24": direction = 1; break;
                        case "Tile_Id25": direction = 3; break;
                        case "Tile_Id26": direction = 2; break;
                        default: break;
                    }

                    if (spriteArray[xPosition, yPosition, layer] == startTile)
                    {
                        edgeTileFound = false;
                    }
                }
            }
            else
            {
                finishedAllChecks = true;
            }

            int listLength = pathPoints.Count;
            Vector2[] pathPointArray = new Vector2[listLength];
            for (int i = 0; i < listLength; i++)
            {
                pathPointArray[i] = pathPoints[i];
            }
            pathList.Add(pathPointArray);
        }

        collider.pathCount = pathList.Count;
        for (int i = 0; i < pathList.Count; i++)
        {
            collider.SetPath(i, pathList[i]);
        }
    }

    //Check if a Vector already exists
    bool CheckIfVectorExists(GameObject tile, List<GameObject> tileList)
    {
        foreach(GameObject currentTile in tileList)
        {
            if(currentTile == tile)
            {
                return true;
            }
        }

        return false;
    }

    public GameObject[,,] GetSpriteArray()
    {
        if (spriteArray == null)
            LoadTiles();
        return spriteArray;
    }


}
