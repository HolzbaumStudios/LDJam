using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{

    public float maxSpeed = 10;
    public float jumpingPower = 400;
    public float waterJumpingPower = 5;
    public float swimmingSpeed = 4;
    public Transform feetCollider;

    private bool grounded; //if the player touches the ground
    public bool swimming = false;
    private bool jumping = false;
    private bool airControl = true; //if the character can be controlled in air
    private Animator playerAnimator;
    private Rigidbody2D rigidbody;
    private bool facingRight = true; //If the character is looking right or left
    private BoxCollider2D boxCollider;
    private CircleCollider2D circleCollider;
    private Vector2 boxColliderSize;
    private Vector2 boxColliderPosition;
    private float circleColliderRadius;
    private Vector2 circleColliderPosition;


    private RaycastHit hit;

    void Start()
    {
        //Get Player Components
        playerAnimator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        circleCollider = GetComponent<CircleCollider2D>();
        //Get Start Values
        boxColliderSize = boxCollider.size;
        boxColliderPosition = boxCollider.offset;
        circleColliderRadius = circleCollider.radius;
        circleColliderPosition = circleCollider.offset;

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

    //move vertical is only used when swimming
    public void Move(float move, bool jump)
    {
        if (grounded || airControl)
        {
            // The Speed animator parameter is set to the absolute value of the horizontal input.
            float speed = Mathf.Abs(move);
            playerAnimator.SetFloat("Speed", speed);
            // Move the character
            float rigidbodySpeed = maxSpeed;
            if (swimming) { rigidbodySpeed = swimmingSpeed;  }
            rigidbody.velocity = new Vector2(move * rigidbodySpeed, rigidbody.velocity.y);

            if (jump && grounded)
            {
                
                playerAnimator.SetBool("Jump", true);
                if (!swimming)
                {
                    rigidbody.AddForce(new Vector2(0f, jumpingPower));
                }
                else
                {
                    if (rigidbody.velocity.y < 3.2f)
                    { 
                        rigidbody.AddForce(new Vector2(0f, waterJumpingPower));
                    }     
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
        Debug.Log("SwimStat: " + swimState);
        swimming = swimState; //Invert Value
        playerAnimator.SetBool("Swimming", swimState);
        if (swimming)
        {
            //Add force up to slow down initial velocity
            float currentVelocity = -rigidbody.velocity.y;
            rigidbody.AddForce(new Vector2(0, currentVelocity * 28));
            rigidbody.gravityScale = 0.05f;
            //Change Collider position and shape
            boxCollider.size = new Vector2(1.2f,1f);
            boxCollider.offset = new Vector2(-0.3f, -0.1f);
            circleCollider.offset = new Vector2(0.78f, 0.2f);
            //Disable Season Change while swimming
        }
        else
        {
            rigidbody.gravityScale = 3;
            boxCollider.offset = boxColliderPosition;
            boxCollider.size = boxColliderSize;
            circleCollider.offset = circleColliderPosition;
        } 
    }

    //returns false if the player is in a position where he can't change season. Is called by changeseason
    public bool ReturnPlayerState()
    {
        if(swimming == true || grounded == false)
        {
            return false;
        }
        return true;
    }

}


