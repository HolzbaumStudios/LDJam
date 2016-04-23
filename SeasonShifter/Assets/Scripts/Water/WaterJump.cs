using UnityEngine;
using System.Collections;

public class WaterJump : MonoBehaviour {

    GameObject player;
    PlayerMovement playerMovementScript;
    public float outOfWaterJumpPower; //Power to jump out of the water


    void Start()
    {
        
    }

   void OnTriggerStay2D(Collider2D col)
    {
        if(Input.GetButtonDown("Jump") && col.CompareTag("Player"))
        {
            col.transform.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, outOfWaterJumpPower));
           // playerMovementScript.ChangeSwimmingState(false);
        }
    }

}
