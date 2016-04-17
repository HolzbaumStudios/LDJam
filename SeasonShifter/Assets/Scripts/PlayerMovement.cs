using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{

    public float maxSpeed = 10;
    public float jumpingPower = 40;
    public float waterJumpingPower = 5;
    public float swimmingSpeed = 4;
    public Transform feetCollider;

    private bool grounded; //if the player touches the ground
    private bool swimming = false;
    private bool jumping = false;
    private bool airControl = true; //if the character can be controlled in air
    private Animator playerAnimator;
    private Rigidbody2D rigidbody;
    private bool facingRight = true; //If the character is looking right or left


    private RaycastHit hit;

    void Start()
    {
        playerAnimator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Check if the character is touching the ground
        if (Physics2D.Raycast(feetCollider.position,-Vector2.up,0.15f) && !jumping)
        {
            grounded = true;
        }
        else
        {
            grounded = false;
        }

        if(!grounded)
        {
            playerAnimator.SetBool("Grounded", false);
        }
        else
        {
            playerAnimator.SetBool("Grounded", true);
        }
    }

    public void Move(float move, bool jump)
    {
        if (grounded || airControl)
        {
            // The Speed animator parameter is set to the absolute value of the horizontal input.
            float speed = Mathf.Abs(move);
            playerAnimator.SetFloat("Speed", speed);
            // Move the character
            rigidbody.velocity = new Vector2(move * maxSpeed, rigidbody.velocity.y);

            if (jump)
            {
                
                playerAnimator.SetBool("Jump", true);
                if (!swimming)
                {
                    rigidbody.AddForce(new Vector2(0f, jumpingPower));
                }
                else
                {
                    rigidbody.AddForce(new Vector2(0f, waterJumpingPower));
                }
            }
            else
            {
                playerAnimator.SetBool("Jump", false);
            }

            if (move > 0 && !facingRight)
            {
                Flip();
            }
            else if (move < 0 && facingRight)
            {
                Flip();
            }
        }
    }

    // Switch the way the player is labelled as facing.
    private void Flip()
    {      
        facingRight = !facingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    public bool ReturnGroundedState()
    {
        return grounded;
    }

    public void ChangeSwimmingState(bool swimState)
    {
        swimming = swimState; //Invert Value
        playerAnimator.SetBool("Swimming", swimState);
        if (swimming)
        {
            //Add force up to slow down initial velocity
            float currentVelocity = rigidbody.velocity.y;
            rigidbody.AddForce(new Vector2(0, currentVelocity*0.8f));
            rigidbody.gravityScale = 0.05f;
        }
        else rigidbody.gravityScale = 3; 
        
    }

}


