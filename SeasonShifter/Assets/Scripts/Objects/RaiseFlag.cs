using UnityEngine;
using System.Collections;

public class RaiseFlag : MonoBehaviour
{
    public bool flagTouched = false;
    public Sprite raisedFlag;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player") && !flagTouched)
        {
            flagTouched = true;
            transform.parent.GetComponent<SpriteRenderer>().sprite = raisedFlag;
            // part where reload is going here
            BoxCollider2D boxCollider = transform.parent.GetComponent<BoxCollider2D>();
            boxCollider.offset = new Vector2(0, 0);
            boxCollider.size = new Vector2(0, 0);
        }
    }
}
