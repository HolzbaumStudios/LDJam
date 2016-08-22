using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerInput : MonoBehaviour {

    PlayerMovement movementScript;
    private bool isJumping = false;
    private bool gliding = false;
    private bool glidingAllowed = false;
    private bool inputEnabled = true;

	// Use this for initialization
	void Awake () {
        movementScript = GetComponent<PlayerMovement>();
        if (PlayerPrefs.GetInt("GlidingAllowed") == 1) glidingAllowed = true;
	}
	

	// Update is called once per frame
	void Update () {
        float v = Input.GetAxis("Vertical"); //the vertical value is only used for swimming
        float h = Input.GetAxis("Horizontal");
        if (Input.GetButtonDown("Jump") && !isJumping)
        {
            isJumping = true;
        }
        //Glider function
        if(Input.GetButton("Fire1"))
        {
            if(glidingAllowed)gliding = true;
        }
        else
        {
            gliding = false;
        }


        // Pass all parameters to the character control script.
        if(inputEnabled)movementScript.Move(h, v, isJumping, gliding);
        isJumping = false;
    }

    public void AllowGliding()
    {
        glidingAllowed = true;
        PlayerPrefs.SetInt("GlidingAllowed", 1);
    }

    public void DisableInput(bool disable)
    {
        inputEnabled = !disable;
    }

}
