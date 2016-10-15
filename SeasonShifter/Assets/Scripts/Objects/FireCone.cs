using UnityEngine;
using System.Collections;

public class FireCone : MonoBehaviour {

    Rigidbody2D rigidbody;
    CircleCollider2D trigger;
    PolygonCollider2D collider;
    Vector3 originalScale;
    private bool onPlatform = false;
    private GameObject platformReference;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        trigger = GetComponent<CircleCollider2D>();
        collider = GetComponent<PolygonCollider2D>();
        originalScale = transform.localScale;
    }

    void FixedUpdate()
    {
        if(onPlatform)
        {
            if(!transform.parent.CompareTag("Platform") || rigidbody.velocity.y < -2)
            {
                platformReference.SendMessage("DisableChange", false);
                platformReference = null;
                onPlatform = false;
            }
        }
    }

	void OnTriggerStay2D(Collider2D col)
    {
        if(col.CompareTag("Player") && Input.GetKeyDown(KeyCode.LeftAlt))
        {
            if(!col.GetComponent<PlayerHolding>().isHolding())
                StartCoroutine(AttachToPlayer(col.transform));
        }
    }

    IEnumerator AttachToPlayer(Transform player)
    {
        Transform staff = GameObject.FindGameObjectWithTag("Staff").transform;
        this.transform.SetParent(staff);
        if(player.localScale.x > 0)
            this.transform.position = staff.position + new Vector3(0.1f, 0.1f);
        else
            this.transform.position = staff.position + new Vector3(-0.1f, 0.1f);
        rigidbody.isKinematic = true;
        trigger.enabled = false;
        collider.enabled = false;
        yield return new WaitForEndOfFrame();
        player.GetComponent<PlayerHolding>().PickUpItem(this.gameObject);
    }

    //Gets called by the script PlayerHolding.
    public void DropItem()
    {
        this.transform.SetParent(null);
        rigidbody.isKinematic = false;
        trigger.enabled = true;
        collider.enabled = true;
        this.transform.localScale = originalScale;
    }

    public void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.CompareTag("Platform"))
        {
            Debug.Log("Enter platform");
            this.transform.SetParent(col.transform);
            col.gameObject.SendMessage("DisableChange", true);
            platformReference = col.gameObject;
            rigidbody.velocity = Vector2.zero;
            onPlatform = true;
        }
    }

}
