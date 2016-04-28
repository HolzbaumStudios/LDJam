using UnityEngine;
using System.Collections;

public class OutOfWaterClimb : MonoBehaviour {

    public bool leftSide = true; //Should be true if exit on the left side, false if exit on the right side
    private bool coroutineStarted = false; //Makes sure that the coroutine only starts once
    Animator playerAnimator;
    void OnTriggerEnter2D(Collider2D col)
    {
        playerAnimator = col.GetComponent<Animator>();
        coroutineStarted = false;
    }

	void OnTriggerStay2D(Collider2D col)
    {
        float xMovement = Input.GetAxis("Horizontal");
        if((xMovement < -0.1f && leftSide) || (xMovement > 0.1f && !leftSide))
        {
            if(!coroutineStarted) StartCoroutine(GetOutOfWater(col.gameObject));
        }
    }

    IEnumerator GetOutOfWater(GameObject player)
    {
        Debug.Log("Started get out of water");
        coroutineStarted = true;

        //Get components
        PlayerInput inputScript = player.GetComponent<PlayerInput>();
        PlayerMovement movementScript = player.GetComponent<PlayerMovement>();
        Rigidbody2D rigidbody = player.GetComponent<Rigidbody2D>();
        BoxCollider2D collider = player.GetComponent<BoxCollider2D>();
        Transform playerBody = player.transform.FindChild("body");

        //Start climb
        rigidbody.gravityScale = 0; //disable gravity
        rigidbody.velocity = Vector2.zero; //Set velocity to zero
        inputScript.DisableInput(true); //Disable all input
        movementScript.enabled = false; //Disable groundcheck preventing going to swiming state midanimation
        playerAnimator.SetBool("OutOfWater", true); //Start climbing animation
        collider.enabled = false;
        player.transform.SetParent(this.transform); //Make player child of collider
        float xPosition = 0.2f;
        if (!leftSide) xPosition *= -1;
        player.transform.localPosition = new Vector2(xPosition, -0.2f);
        playerAnimator.SetBool("OutOfWater", true); //Start climbing animation
        yield return new WaitForSeconds(0.75f);

        //End climb
        player.transform.position = playerBody.position;
        playerAnimator.SetBool("OutOfWater", false);
        playerBody.localPosition = new Vector2(0, 0);
        collider.enabled = true;
        player.transform.SetParent(null);
        inputScript.DisableInput(false);
        movementScript.enabled = true;
        movementScript.ChangeSwimmingState(false);
    }
}
