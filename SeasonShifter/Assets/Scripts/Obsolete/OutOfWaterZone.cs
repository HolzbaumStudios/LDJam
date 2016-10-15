using UnityEngine;
using System.Collections;

public class OutOfWaterZone : MonoBehaviour
{
    GameObject player;
    PlayerMovement playerMovementScript;
    private float initialJumpingPower;

    void Start()
    {
        player = GameObject.Find("Player");
        playerMovementScript = player.GetComponent<PlayerMovement>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player") && playerMovementScript.GetSwimmingState())
        {
            playerMovementScript.ChangeSwimmingState(false);
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player") && playerMovementScript.GetSwimmingState())
        {
            playerMovementScript.ChangeSwimmingState(false);
        }
    }
}
