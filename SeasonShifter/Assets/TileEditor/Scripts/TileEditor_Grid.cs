using UnityEngine;
using UnityEditor.SceneManagement;
using System.Collections;

[RequireComponent(typeof(TileEditor_ObjectHandler))]
[RequireComponent(typeof(TileEditor_DisplayBrushCollection))]
[ExecuteInEditMode]
public class TileEditor_Grid : MonoBehaviour {

    public float lineHeight = 1f;
    public float lineWidth = 1f;
    public float gridHeight = 50f;
    public float gridWidth = 50f;


    public bool editorEnabled = false;


    public Color color = Color.white;

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

    public void EnableEditor()
    {
        editorEnabled = !editorEnabled;
        EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene()); //Marks scene as dirty, so that editor changes are saved
    }

    public void LoadTiles()
    {
        this.gameObject.GetComponent<TileEditor_ObjectHandler>().LoadTiles();
    }

    public void CreateCollider()
    {
        this.gameObject.GetComponent<TileEditor_ObjectHandler>().CreateCollider();
    }

}
