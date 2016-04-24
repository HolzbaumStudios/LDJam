using UnityEngine;
using System.Collections;

public class SwitchScript : MonoBehaviour {

    public bool switchPressed = false;
    public BridgeMovement target;
    public PlattformMovement target2;
    public Sprite pressedButton;

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.CompareTag("Player") && !switchPressed)
        {
            switchPressed = true;
            transform.parent.GetComponent<SpriteRenderer>().sprite = pressedButton;
            if(target != null) target.enabled = true;
            if(target2 != null) target2.enabled = true;
            BoxCollider2D boxCollider = transform.parent.GetComponent<BoxCollider2D>();
            boxCollider.offset = new Vector2(0, -1.4f);
            boxCollider.size = new Vector2(0.7f, 0.15f);
        }
    }
}
