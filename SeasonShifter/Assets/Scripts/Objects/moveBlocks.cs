using UnityEngine;
using System.Collections;

public class moveBlocks : MonoBehaviour {
    // Variablen
    private Vector3 originalScale;
    private Rigidbody2D rigidbody;
    private BoxCollider2D collider;
    private Vector2 originalColliderSize;
    private PolygonCollider2D trigger;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        originalScale = transform.localScale;
        trigger = GetComponent<PolygonCollider2D>();
        collider = GetComponent<BoxCollider2D>();
        originalColliderSize = collider.size;
    }

    private RaycastHit2D hit;

    void Update()
    {
        //Check if the box went over the edge. If it does, detach it from the player and enable physics
        hit = Physics2D.Raycast(transform.position, -Vector2.up, 0.4f);
        if(rigidbody.isKinematic == true && !hit)
        {
            Debug.Log("No hit");
            if(transform.parent.CompareTag("Player"))
            {
                this.transform.parent.GetComponent<PlayerHolding>().DropItem();
                rigidbody.isKinematic = false;
            }
        }
        else if(hit && !rigidbody.isKinematic)
        {
            rigidbody.isKinematic = true;
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.CompareTag("Player") && Input.GetKeyDown(KeyCode.LeftAlt))
        {
            if (!col.GetComponent<PlayerHolding>().isHolding())
                StartCoroutine(AttachToPlayer(col.transform));
        }
    }

    IEnumerator AttachToPlayer(Transform player)
    {
        collider.size = originalColliderSize - new Vector2(0.1f, 0.1f);
        this.transform.SetParent(player);
        rigidbody.isKinematic = true;
        trigger.enabled = false;
        yield return new WaitForEndOfFrame();
        player.GetComponent<PlayerHolding>().PickUpItem(this.gameObject, false);
        PlayerMovement playerMovement = player.GetComponent<PlayerMovement>();
        playerMovement.DisableJumping(true);
        playerMovement.DisableTurning(true);
    }

    //Gets called by the script PlayerHolding.
    public void DropItem()
    {
        collider.size = originalColliderSize;
        Transform player = this.transform.parent;
        this.transform.SetParent(null);
        trigger.enabled = true;
        this.transform.localScale = originalScale;
        PlayerMovement playerMovement = player.GetComponent<PlayerMovement>();
        playerMovement.DisableJumping(false);
        playerMovement.DisableTurning(false);
    }
}
