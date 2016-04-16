using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{

    public float maxSpeed = 10;
    public float jumpingPower = 20;

    private bool grounded; //if the player touches the ground
    private bool airControl = true; //if the character can be controlled in air
    private Animator playerAnimator;
    private Rigidbody2D rigidbody;
    private bool facingRight = true; //If the character is looking right or left
    

    void Start()
    {
        playerAnimator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
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

            if(move > 0 && !facingRight)
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
}


