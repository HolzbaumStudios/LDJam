using UnityEngine;
using System.Collections;

public class OutOfWaterClimb : MonoBehaviour {

    public bool leftSide = true; //Should be true if exit on the left side, false if exit on the right side
    Animator playerAnimator;
    void OnTriggerEnter2D(Collider2D col)
    {
        playerAnimator = col.GetComponent<Animator>();
    }

	void OnTriggerStay2D(Collider2D col)
    {
        float xMovement = Input.GetAxis("Horizontal");
        if((xMovement < -0.1f && leftSide) || (xMovement > 0.1f && !leftSide))
        {
            playerAnimator.SetBool("OutOfWater", true);
        }
            

        
    }
}
