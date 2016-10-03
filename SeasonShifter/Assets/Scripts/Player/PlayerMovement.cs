using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{

    public float maxSpeed = 6;
    public float jumpingPower = 380;
    public float waterJumpingPower = 50;
    public float swimmingSpeed = 3;
    private float playerGravityScale = 2f;
    public Transform rightHand;
    public Transform feetCollider;
    private SeasonManager seasonManager;
    private GameProgress gameProgress;

    private bool grounded; //if the player touches the ground
    private bool swimming = false;
    private bool allowUpSwimming = true; //if the player is out of the collider, upswimming will be disabled
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
    private bool freezedPosition = false;
    private bool disabledRaycast = false; //After a jump, the raycast is disabled for 0.1 seconds

    RaycastHit2D hit;

    ///Variables only called by Player Input. These should not be changed within this code
    private bool jump;
    private float move;

    void Start()
    {
        //Get Player Components
        playerAnimator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        circleCollider = GetComponent<CircleCollider2D>();
        soundScript = GetComponent<PlayerSound>();

        seasonManager = GameObject.Find("LevelManager").GetComponent<SeasonManager>();
        gameProgress = GameManager.gameProgressInstance;

        //Get Start Values
        boxColliderSize = boxCollider.size;
        boxColliderPosition = boxCollider.offset;

    }


    //Check if the state changes
    void FixedUpdate()
    {
        
        //Do a raycast to check if it hits something
        hit = Physics2D.Raycast(feetCollider.position, -Vector2.up, 0.12f);
        //Check if the ground is hit
        if (hit && !disabledRaycast)
        {
            if (hit.collider.CompareTag("Water") && !swimming)
            {
                grounded = false;
                ChangeSwimmingState(true);
            }
            else if(hit.collider.CompareTag("Trampoline") && rigidbody.velocity.y < 0)
            {
                disabledRaycast = true;
                rigidbody.velocity = new Vector2(rigidbody.velocity.x, 0);
                rigidbody.AddForce(new Vector2(0, jumpingPower * 2));
                StartCoroutine(EnableRaycast());
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
        if(swimming && !hit)
        {
            //Raycast down to check if you're on the top of the water
            RaycastHit2D swimmingHit = Physics2D.Raycast(feetCollider.position, -Vector2.up, 0.2f);
            if (!swimmingHit)
                allowUpSwimming = false;
            else if (!allowUpSwimming && swimmingHit)
                allowUpSwimming = true;

            //Raycast to side
            Vector2 direction = Vector2.right;
            if (!facingRight)
                direction = Vector2.left;
            RaycastHit2D sideHit = Physics2D.Raycast(this.gameObject.transform.position, direction, 1f);
            if (sideHit)
            {
                //Checks if it is possible to climb out at this position
                RaycastHit2D topSideHit = Physics2D.Raycast(this.gameObject.transform.position + new Vector3(0, 1), direction, 1); ;
                if (facingRight && rigidbody.velocity.x > 0.1f && !topSideHit)
                    ClimbOutOfWater(false);
                else if (!facingRight && rigidbody.velocity.x < -0.1f && !topSideHit)
                    ClimbOutOfWater(true);
            } 
        }

        //Set animation bool
        if (grounded)
        {
            playerAnimator.SetBool("Grounded", true);
            if (!hit.transform.CompareTag("Platform"))
            {
                if ((hit.normal.x > 0.75f || hit.normal.x < -0.75f) && hit.normal.y > 0.75f && freezedPosition)
                {
                    freezedPosition = false;
                    rigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
                }
                else if ((hit.normal.x > 0.6f || hit.normal.x < -0.6f) && hit.normal.y > 0.6f)
                    NormalizeSlope(hit.normal); //Normalize movement when standing on a slope
                else if (freezedPosition)
                {
                    freezedPosition = false;
                    rigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
                }
            }
        }
        else
            playerAnimator.SetBool("Grounded", false);
    }

    //move is called by the player input script
    public void Move(float move, float moveVertical, bool jump, bool staffAction) //StaffAction = is controll pressed
    {
        // The Speed animator parameter is set to the absolute value of the horizontal input.
        this.move = move;
        this.jump = jump;
        float speed = Mathf.Abs(move);
        if((speed != 0 || jump) && freezedPosition) //Chech if position is freezed, because the player is on a slope
        {
            freezedPosition = false;
            rigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
        playerAnimator.SetFloat("Speed", speed);
        //Set the speed of the rigidbody
        float rigidbodySpeed = maxSpeed;
        float rigidbodyVerticalSpeed = rigidbody.velocity.y;
        if (swimming)
        {
            rigidbodySpeed = swimmingSpeed;
            if (!allowUpSwimming && moveVertical > 0) //if player reaches top of water, he can't move up
            {
                rigidbody.gravityScale = 0; //the gravity scale is set to 0 to prevent the player from 'stuttering'
                moveVertical = 0;
            }
            else if (rigidbody.gravityScale == 0)
                rigidbody.gravityScale = playerGravityScale;

            rigidbodyVerticalSpeed = moveVertical * (swimmingSpeed / 2);
        }
        //Move the player
        rigidbody.velocity = new Vector2(move * rigidbodySpeed, rigidbodyVerticalSpeed);
        //Jump
        if (jump && grounded && !swimming)
        {
            rigidbody.velocity = new Vector2(rigidbody.velocity.x, 0); //set y velocitiy to 0, ensuring that the player always reaches the same height while jumping
            disabledRaycast = true;
            playerAnimator.SetBool("Jump", true); //Change this to a trigger !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            rigidbody.AddForce(new Vector2(0f, jumpingPower));
            soundScript.Jump();
            StartCoroutine(EnableRaycast()); //Enable the raycast ground check after 0.1 secods
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
        if(glide && (!isGliding && !grounded && !swimming) && gameProgress.umbrella)
        {
            rightHand.FindChild("staff").gameObject.SetActive(false);
            rightHand.FindChild("staff_sum_umbrella").gameObject.SetActive(true);
            playerAnimator.SetBool("Gliding", true);
            rigidbody.gravityScale = 0.15f;
            rigidbody.velocity = new Vector2(rigidbody.velocity.x, -0.2f);
            isGliding = true;
        }
        else if((glide && isGliding && (grounded || swimming)) || (!glide && isGliding))
        {
            rightHand.FindChild("staff").gameObject.SetActive(true);
            rightHand.FindChild("staff_sum_umbrella").gameObject.SetActive(false);
            playerAnimator.SetBool("Gliding", false);
            rigidbody.gravityScale = playerGravityScale;
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

    public bool GetGlidingState()
    {
        return isGliding;
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
            //Change Collider position and shape
            boxCollider.size = new Vector2(2.1f, 1.6f);
            boxCollider.offset = new Vector2(0f, -0.4f);
        }
        else
        {
            boxCollider.offset = boxColliderPosition;
            boxCollider.size = boxColliderSize;
            rigidbody.gravityScale = playerGravityScale;
            allowUpSwimming = true;
        }
    }

    //Start the function to get out of water -> Function has to be scripted still
    public void ClimbOutOfWater(bool leftSide)
    {
        float xValue = 0.1f;
        if (leftSide) xValue *= -1;
        rigidbody.transform.position += new Vector3(xValue, 0.9f);
        ChangeSwimmingState(false);
    }

    //Prvent Sliding on slopes, by disabling rigidbody movement, while not moving
    void NormalizeSlope(Vector2 normalValues)
    {
        if (rigidbody.constraints != RigidbodyConstraints2D.FreezePositionX && !jump && move == 0)
        {
            freezedPosition = true;
            rigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }

    IEnumerator EnableRaycast()
    {
        yield return new WaitForSeconds(0.1f);
        disabledRaycast = false;
    }
}


