using UnityEngine;
using System.Collections;

public class WindForce : MonoBehaviour {

    Rigidbody2D rigidbody;
    PlayerMovement playerMovement;

    void OnTriggerEnter2D(Collider2D col)
    {
        rigidbody = col.GetComponent<Rigidbody2D>();
        if (col.CompareTag("Player"))
            playerMovement = col.GetComponent<PlayerMovement>();
    }

	void OnTriggerStay2D(Collider2D col)
    {
        if (col.CompareTag("Player") && playerMovement.GetGlidingState())
        {
            rigidbody.AddForce(new Vector2(0, 4), ForceMode2D.Force);
        }
    }
}
