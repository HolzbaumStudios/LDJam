using UnityEngine;
using System.Collections;

public class OutOfWaterClimb2 : MonoBehaviour {

    public bool leftSide = true; //Should be true if exit on the left side, false if exit on the right side
    private bool coroutineStarted = false; //Makes sure that the coroutine only starts once
    Animator playerAnimator;
    Rigidbody2D rigidbody;
    Vector2 targetPosition;
    int movementStep = 0; //Which movement step is initialized for the player


    public void GetOutOfWater(GameObject player, bool leftSide)
    {
        rigidbody = player.GetComponent<Rigidbody2D>();
        playerAnimator = rigidbody.gameObject.GetComponent<Animator>();
        coroutineStarted = false;
        float xValue = 0.2f;
        if (leftSide) xValue *= -1;
        targetPosition = rigidbody.transform.position + new Vector3(xValue, 1.2f);
        StartCoroutine(GetOutOfWater(player));
    }

    void Update()
    {
        if (movementStep == 1)
        {
            rigidbody.position = Vector2.Lerp(rigidbody.position, transform.position + new Vector3(0,0.3f), Time.deltaTime * 8);
        }
        else if(movementStep == 2)
        {
            rigidbody.position = Vector2.Lerp(rigidbody.position, targetPosition, Time.deltaTime * 10);
        }
    }

    IEnumerator GetOutOfWater(GameObject player)
    {
        coroutineStarted = true;

        //Get components
        PlayerInput inputScript = player.GetComponent<PlayerInput>();
        PlayerMovement movementScript = player.GetComponent<PlayerMovement>();
        
        BoxCollider2D collider = player.GetComponent<BoxCollider2D>();
        Transform playerBody = player.transform.Find("body");

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
        playerAnimator.SetBool("Swimming", false);
        playerAnimator.Play("Player_GetOutOfWater");
        yield return new WaitForSeconds(0.1f);
        movementStep = 1;
        yield return new WaitForSeconds(0.2f);
        movementStep = 2;
        yield return new WaitForSeconds(0.6f);

        //End climb
        movementStep = 0;
        playerAnimator.SetBool("OutOfWater", false);
        coroutineStarted = false;
        collider.enabled = true;
        player.transform.SetParent(null);
        inputScript.DisableInput(false);
        movementScript.enabled = true;
        movementScript.ChangeSwimmingState(false);
    }
}
