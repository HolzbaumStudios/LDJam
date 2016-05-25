using UnityEngine;
using System.Collections;

public class TileEditor_Grid : MonoBehaviour {

    public float height = 1f;
    public float width = 1f;

    public bool editorEnabled = false;

    public Color color = Color.white;


    void OnDrawGizmos()
    {
        if (editorEnabled)
        {
            Vector3 pos = Camera.current.transform.position;
            Gizmos.color = color;

            for (float y = pos.y - 800.0f; y < pos.y + 800.0f; y += height)
            {
                Gizmos.DrawLine(new Vector3(-1000000.0f, Mathf.Floor(y / height) * height, 0.0f),
                                new Vector3(1000000.0f, Mathf.Floor(y / height) * height, 0.0f));
            }

            for (float x = pos.x - 1200.0f; x < pos.x + 1200.0f; x += width)
            {
                Gizmos.DrawLine(new Vector3(Mathf.Floor(x / width) * width, -1000000.0f, 0.0f),
                                new Vector3(Mathf.Floor(x / width) * width, 1000000.0f, 0.0f));
            }
        }
    }
}
