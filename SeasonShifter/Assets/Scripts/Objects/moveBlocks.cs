using UnityEngine;
using System.Collections;

public class moveBlocks : MonoBehaviour {
    // Variablen
    private bool fPressed = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.F))
        {
            Debug.Log("F key was pressed in update");
            fPressed = true;
        }
        else
        {
            Debug.Log("F key is not pressed anymore in update");
            fPressed = false;
            this.transform.SetParent(null);
            Debug.Log("Box has no more parent =(");
        }
    }

    // On Collision check if it's Player   
    void OnCollisionStay2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player hit the box");
            if (fPressed == true)
            {
                this.transform.SetParent(coll.transform);
                Debug.Log("Box has a new parent");
                coll.transform.GetComponent<PlayerMovement>().maxSpeed = 1;
                coll.transform.GetComponent<PlayerMovement>().jumpingPower = 0;
                Debug.Log("speed is now 1");
            }
            else
            {
                coll.transform.GetComponent<PlayerMovement>().maxSpeed = 10;
                coll.transform.GetComponent<PlayerMovement>().jumpingPower = 400;
                Debug.Log("speed is 10 again");
            }
        }
    }	
}
