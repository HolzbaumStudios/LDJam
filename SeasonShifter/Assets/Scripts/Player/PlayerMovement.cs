using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{

    public float maxSpeed = 10;
    public float jumpingPower = 400;
    public float waterJumpingPower = 50;
    public float swimmingSpeed = 4;
    public Transform rightHand;
    public Transform feetCollider;
    private SeasonManager seasonManager;

    private bool grounded; //if the player touches the ground
    private bool swimming = false;
    private bool disabledWaterJump = false; //If this is set to true, player can't rise higher on the water
    private bool isGliding = false;
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
    private bool groundCheckEnabled = true;


    private RaycastHit hit;

    void Start()
    {
        //Get Player Components
        playerAnimator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        circleCollider = GetComponent<CircleCollider2D>();
        soundScript = GetComponent<PlayerSound>();

        seasonManager = GameObject.Find("LevelManager").GetComponent<SeasonManager>();

        //Get Start Values
        boxColliderSize = boxCollider.size;
        boxColliderPosition = boxCollider.offset;

    }

    //Check if the state changes
    void FixedUpdate()
    {
        //Do a raycast to check if it hits something
        RaycastHit2D hit = Physics2D.Raycast(feetCollider.position, -Vector2.up, 0.12f);
        //Check if the ground is hit
        if (hit)
        {
            if (hit.collider.CompareTag("Water") && !swimming)
            {
                grounded = false;
                ChangeSwimmingState(true);

            }
            else if (!grounded)
            {
                soundScript.Land();
                grounded = true;
            }
        }
        else
        {
            grounded = false; // if collider doesn't hit, set grounded to false
        }

        //If swimming, check when out of water
        if(swimming)
        {
            RaycastHit2D upHit = Physics2D.Raycast(feetCollider.position + new Vector3(0, 0.6f, 0), Vector2.up, 0.1f);

            if (!upHit && !disabledWaterJump)
            {
                disabledWaterJump = true;
                rigidbody.velocity = new Vector2(rigidbody.velocity.x, 0);
                Debug.Log(rigidbody.velocity);

            }
            else if(upHit && disabledWaterJump)
            {
                disabledWaterJump = false;
            }
        }

        //Set animation bool
        if(grounded)
            playerAnimator.SetBool("Grounded", true);
        else
            playerAnimator.SetBool("Grounded", false);
    }

    //move is called by the player input script
    public void Move(float move, bool jump, bool staffAction) //StaffAction = is controll pressed
    {
        // The Speed animator parameter is set to the absolute value of the horizontal input.
        float speed = Mathf.Abs(move);
        playerAnimator.SetFloat("Speed", speed);
        //Set the speed of the rigidbody
        float rigidbodySpeed = maxSpeed;
        if (swimming)
            rigidbodySpeed = swimmingSpeed;
        //Move the player
        rigidbody.velocity = new Vector2(move * rigidbodySpeed, rigidbody.velocity.y);
        //Jump
        if (jump && (grounded || swimming))
        {
            playerAnimator.SetBool("Jump", true); //Change this to a trigger !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            if (!swimming)
            {
                rigidbody.AddForce(new Vector2(0f, jumpingPower));
                soundScript.Jump();
            }
            else if (rigidbody.velocity.y < 3.8f && !disabledWaterJump) //Jump in the water, but check that the speed doesn't get too high
            {
                rigidbody.AddForce(new Vector2(0f, waterJumpingPower));
            }
        }
        
        //Check if staff action is used
        if(seasonManager.currentSeason == SeasonManager.Season.fall)
            Glide(staffAction); //Glide

        ///Set facing direction/////////////////
        if ((move > 0 && !facingRight) || (move < 0 && facingRight))
            Flip();

    }

    //All the mechanics of gliding
    public void Glide(bool glide)
    {
        if(glide && (!isGliding && !grounded && !swimming))
        {
            rightHand.FindChild("staff").gameObject.SetActive(false);
            rightHand.FindChild("staff_sum_umbrella").gameObject.SetActive(true);
            playerAnimator.SetBool("Gliding", true);
            rigidbody.gravityScale = 0.45f;
            rigidbody.velocity = new Vector2(rigidbody.velocity.x, 0);
            isGliding = true;
        }
        else if((glide && isGliding && (grounded || swimming)) || (!glide && isGliding))
        {
            rightHand.FindChild("staff").gameObject.SetActive(true);
            rightHand.FindChild("staff_sum_umbrella").gameObject.SetActive(false);
            playerAnimator.SetBool("Gliding", false);
            if (swimming) rigidbody.gravityScale = 0.1f; else rigidbody.gravityScale = 3;
            isGliding = false;
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

    //Get Player States
    public bool GetGroundedState()
    {
        return grounded;
    }

    public bool GetSwimmingState()
    {
        return swimming;
    }

    //Change swimming state
    public void ChangeSwimmingState(bool swimState)
    {
        swimming = swimState; //Invert Value
        playerAnimator.SetBool("Swimming", swimState);
        if (swimming)
        {
            //Add force up to slow down initial velocity
            float currentVelocity = -rigidbody.velocity.y;
            rigidbody.gravityScale = 0.1f;
            //Change Collider position and shape
            boxCollider.size = new Vector2(2.1f, 1.6f);
            boxCollider.offset = new Vector2(0f, -0.4f);
        }
        else
        {
            rigidbody.gravityScale = 3;
            boxCollider.offset = boxColliderPosition;
            boxCollider.size = boxColliderSize;
        }
    }

}


