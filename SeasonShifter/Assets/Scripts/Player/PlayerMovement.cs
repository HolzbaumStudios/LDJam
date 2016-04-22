using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{

    public float maxSpeed = 10;
    public float jumpingPower = 400;
    public float waterJumpingPower = 5;
    public float swimmingSpeed = 4;
    public Transform rightHand;
    public Transform feetCollider;

    enum Season { spring, summer, fall, winter };
    Season currentSeason = Season.summer;
    private ChangeSeason seasonManager;

    private bool grounded; //if the player touches the ground
    public bool swimming = false;
    private bool jumping = false;
    private bool isGliding = false;
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
    private PlayerSound soundScript;


    private RaycastHit hit;

    void Start()
    {
        //Get Player Components
        playerAnimator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        circleCollider = GetComponent<CircleCollider2D>();
        soundScript = GetComponent<PlayerSound>();
        //Get other components
        seasonManager = GameObject.Find("GameManager").GetComponent<ChangeSeason>();
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
        RaycastHit2D hit = Physics2D.Raycast(feetCollider.position, -Vector2.up, 0.12f) ;
        if (hit && !jumping)
        {
            if(hit.collider.CompareTag("Water"))
            {
                if (!swimming)
                {
                    grounded = false;
                    ChangeSwimmingState(true);
                }
            }
            else //if ground
            {
                if (swimming) ChangeSwimmingState(false);
                if (!grounded) soundScript.Land();
                grounded = true;
            }
        }
        else
        {
            grounded = false;
            if (swimming) ChangeSwimmingState(false);
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
    public void Move(float move, bool jump, bool gliding)
    {
        if (grounded || airControl || swimming)
        {
            // The Speed animator parameter is set to the absolute value of the horizontal input.
            float speed = Mathf.Abs(move);
            playerAnimator.SetFloat("Speed", speed);
            // Move the character
            float rigidbodySpeed = maxSpeed;
            if (swimming) { rigidbodySpeed = swimmingSpeed;  }
            rigidbody.velocity = new Vector2(move * rigidbodySpeed, rigidbody.velocity.y);

            if (jump && (grounded || swimming))
            {
                playerAnimator.SetBool("Jump", true);
                if (!swimming)
                {
                    rigidbody.AddForce(new Vector2(0f, jumpingPower));
                    soundScript.Jump();
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


            ////Check gliding state////////////////
            if(isGliding && gliding)
            {
                float currentVelocity = -rigidbody.velocity.y;
                Debug.Log(currentVelocity);
                if (currentVelocity > 3.5f)rigidbody.AddForce(new Vector2(0, currentVelocity*20));
            }
            if (gliding && !isGliding && !grounded)
            {
                int seasonNumber = seasonManager.GetSeason();
                if ((Season)seasonNumber == Season.summer)
                {
                    rightHand.FindChild("staff").gameObject.SetActive(false);
                    rightHand.FindChild("staff_sum_umbrella").gameObject.SetActive(true);
                    playerAnimator.SetBool("Gliding", true);
                    rigidbody.gravityScale = 0.35f;
                    isGliding = gliding;
                }
            }
            if((gliding && isGliding && grounded) || (!gliding && isGliding))
            {
                rightHand.FindChild("staff").gameObject.SetActive(true);
                rightHand.FindChild("staff_sum_umbrella").gameObject.SetActive(false);
                playerAnimator.SetBool("Gliding", false);
                rigidbody.gravityScale = 3;
                isGliding = false;
            }

            ///Set facing direction/////////////////
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
            boxCollider.size = new Vector2(1.2f,1.6f);
            boxCollider.offset = new Vector2(-0.3f, -0.4f);
            circleCollider.offset = new Vector2(0.78f, 0.2f);
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


