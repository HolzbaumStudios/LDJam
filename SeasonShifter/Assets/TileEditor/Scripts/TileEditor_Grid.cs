using UnityEngine;
using System.Collections;

[RequireComponent(typeof(TileEditor_ObjectHandler))]
public class TileEditor_Grid : MonoBehaviour {

    public float lineHeight = 1f;
    public float lineWidth = 1f;
    public float gridHeight = 50f;
    public float gridWidth = 50f;


    public bool editorEnabled = false;

    public Color color = Color.white;


    void OnDrawGizmos()
    {
        if (editorEnabled)
        {
            Vector3 pos = Camera.current.transform.position;
            Gizmos.color = color;

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

    public void EnableEditor()
    {
        editorEnabled = !editorEnabled;
    }
}
