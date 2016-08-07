using UnityEngine;
using System.Collections;

public class moveBlocks : MonoBehaviour {
    // Variablen
    private bool altPressed = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("left alt"))
        {
            Debug.Log("Alt left key was pressed in update");
            altPressed = true;
        }
        else
        {
            Debug.Log("Alt left key is not pressed anymore in update");
            altPressed = false;
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
            if (altPressed == true)
            {
                this.transform.SetParent(coll.transform);
                Debug.Log("Box has a new parent");
                coll.transform.GetComponent<PlayerMovement>().maxSpeed = 1;
                Debug.Log("speed is now 1");
            }
            else
            {
                coll.transform.GetComponent<PlayerMovement>().maxSpeed = 10;
                Debug.Log("speed is 10 again");
            }
        }
    }	
}
