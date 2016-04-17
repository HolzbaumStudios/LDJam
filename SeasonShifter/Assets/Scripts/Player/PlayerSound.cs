using UnityEngine;
using System.Collections;

public class PlayerSound : MonoBehaviour {

    AudioSource playerAudio;
    public AudioClip[] sounds; //0 = footsteps, 1 = Jump, 2 = Underwater, 3 = Land
    enum AnimatorStates { idle, run, jump, swim}
    AnimatorStates currentAnimatorState = AnimatorStates.idle;
    PlayerMovement playerMovementScript;
    Rigidbody2D player;

    private bool grounded;
    private bool swimming;

	// Use this for initialization
	void Start () {
        playerAudio = GetComponent<AudioSource>();
        playerMovementScript = GetComponent<PlayerMovement>();
        player = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        Debug.Log(currentAnimatorState);
        grounded = playerMovementScript.ReturnGroundedState();
        swimming = playerMovementScript.swimming;

        if(swimming && currentAnimatorState != AnimatorStates.swim)
        {
            currentAnimatorState = AnimatorStates.swim;
            playerAudio.loop = true;
            playerAudio.pitch = 0.7f;
            playerAudio.volume = 0.9f;
            playerAudio.clip = sounds[2];
            playerAudio.Play();
        }
        else if((player.velocity.x > 4f || player.velocity.x < -4f) && grounded && currentAnimatorState != AnimatorStates.run)
        {
            currentAnimatorState = AnimatorStates.run;
            playerAudio.loop = true;
            playerAudio.pitch = 0.4f;
            playerAudio.volume = 0.3f;
            playerAudio.clip = sounds[0];
            playerAudio.Play();
        }
        else if(((player.velocity.x < 4f && player.velocity.x > -4f)  || !grounded) && !swimming && currentAnimatorState != AnimatorStates.idle)
        {
            currentAnimatorState = AnimatorStates.idle;
            playerAudio.Stop();
        }
    }


    public void Jump()
    {
        playerAudio.volume = 0.5f;
       // playerAudio.PlayOneShot(sounds[1], 1);
    }

    public void Land()
    {
        playerAudio.loop = false;
        playerAudio.pitch = 1.9f;
        playerAudio.volume = 0.05f;
        playerAudio.PlayOneShot(sounds[3], 0.8f);
    }

}
