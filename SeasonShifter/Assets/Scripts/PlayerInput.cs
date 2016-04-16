using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerInput : MonoBehaviour {

    PlayerMovement movementScript;
    private bool isJumping = false;

	// Use this for initialization
	void Awake () {
        movementScript = GetComponent<PlayerMovement>();
	}
	
	// Update is called once per frame
	void Update () {

        float h = Input.GetAxis("Horizontal");
        if (Input.GetButtonDown("Jump") && !isJumping)
        {
            isJumping = true;
            StartCoroutine(DisableIsJumping());
        }
        // Pass all parameters to the character control script.
        movementScript.Move(h, isJumping);
    }

    IEnumerator DisableIsJumping()
    {
        yield return new WaitForSeconds(0.3f);
        isJumping = false;
    }
}
