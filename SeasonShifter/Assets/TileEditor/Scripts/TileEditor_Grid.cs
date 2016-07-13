using UnityEngine;
using UnityEditor.SceneManagement;
using System.Collections;
using System;
using UnityEditorInternal;
using System.Reflection;

[ExecuteInEditMode]
public class TileEditor_Grid : MonoBehaviour {

    public float lineHeight = 1f;
    public float lineWidth = 1f;
    public float gridHeight = 50f;
    public float gridWidth = 50f;


    public bool editorEnabled = false;

    //For brushes and single sprites
    public string[] sortingLayers;
    public int sortingLayerIndex;
    public int orderInLayer;
    public Material material;
    public bool flipX = false;
    public bool flipY = false;

    //For single sprites
    public string[] colliderType = new string[] { "None", "Box Collider 2D", "Circle Collider 2D", "Polygon Collider 2D" };
    public int colliderTypeIndex;
    public bool isTrigger = false;
    public Vector2 boxColliderSize = new Vector2(1,1);
    public Vector2 boxColliderOffset = new Vector2(0,0);


    public Color color = Color.white;

    public Color defaultButtonBackground = new Color32(255,255,255,255);
    public Color selectedButtonBackground = new Color32(255,255,255,255);
    

    //Constructor
    public TileEditor_Grid()
    {
        editorEnabled = false;
    }

    void Start()
    {
        editorEnabled = false;
    }

    //Is called by tileeditor_menu.cs
    public void InitializeGrid(int x, int y)
    {
        gridHeight = y;
        gridWidth = x;
    }

    void OnDrawGizmos()
    {
        if (editorEnabled)
        {
            Vector3 pos = Camera.current.transform.position;
            Gizmos.color = color;
            
            //GRID
            for (float y = 0; y < gridHeight; y += lineHeight)
            {
                Gizmos.DrawLine(new Vector3(0, Mathf.Floor(y / lineHeight) * lineHeight, 0.0f),
                                new Vector3(gridWidth, Mathf.Floor(y / lineHeight) * lineHeight, 0.0f));
            }

            for (float x = 0; x < gridWidth; x += lineWidth)
            {
                Gizmos.DrawLine(new Vector3(Mathf.Floor(x / lineWidth) * lineWidth, 0, 0.0f),
                                new Vector3(Mathf.Floor(x / lineWidth) * lineWidth, gridHeight, 0.0f));
            }
        }
    }

    public void AddSprite(Vector3 position)
    {
        this.gameObject.GetComponent<TileEditor_ObjectHandler>().AddSprite(position);
    }

    public void RemoveSprite(Vector3 position)
    {
        this.gameObject.GetComponent<TileEditor_ObjectHandler>().RemoveSprite(position);
    }

    public void FillArea(Vector3 position)
    {
        this.gameObject.GetComponent<TileEditor_ObjectHandler>().FillArea(position);
    }

    public void SelectArea(Vector2 startingPoint, Vector2 endPoint, bool erase)
    {
        this.gameObject.GetComponent<TileEditor_ObjectHandler>().SelectArea(startingPoint, endPoint, erase);
    }

    public void EnableEditor()
    {
        editorEnabled = !editorEnabled;
        EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene()); //Marks scene as dirty, so that editor changes are saved
        if (editorEnabled) RetrieveInformation(); //Udate information like sorting layers and materials
    }

    public void LoadTiles()
    {
        this.gameObject.GetComponent<TileEditor_ObjectHandler>().LoadTiles();
    }

    public void CreateCollider()
    {
        this.gameObject.GetComponent<TileEditor_ObjectHandler>().CreateCollider();
    }

    //Retrieve information about sorting layers and materials
    public void RetrieveInformation()
    {
        //Sorting layers
        /*string[] tempArray = GetSortingLayerNames();
        int arrayLength = tempArray.Length + 1;
        sortingLayers = new string[arrayLength];
        sortingLayers[0] = "Add new Layer...";
        for (int x = 1; x < arrayLength; x++)
        {
            sortingLayers[x] = tempArray[x - 1];
        }*/

        sortingLayers = GetSortingLayerNames();

        //Materials
        if(material == null)
        {
            material = new SpriteRenderer().material;
        }
    }

    public string[] GetSortingLayerNames()
    {
        Type internalEditorUtilityType = typeof(InternalEditorUtility);
        PropertyInfo sortingLayersProperty = internalEditorUtilityType.GetProperty("sortingLayerNames", BindingFlags.Static | BindingFlags.NonPublic);
        return (string[])sortingLayersProperty.GetValue(null, new object[0]);
    }


    public void SetObjectInformation()
    {
        TileEditor_ObjectHandler objectHandler = this.gameObject.GetComponent<TileEditor_ObjectHandler>();
        string name = sortingLayers[sortingLayerIndex];
        objectHandler.SetSortingLayer(name, orderInLayer);
        objectHandler.SetMaterial(material);
        objectHandler.SetFlipInformation(flipX, flipY);
        objectHandler.SetColliderInformation(colliderTypeIndex, isTrigger, boxColliderSize, boxColliderOffset);
    }

}
