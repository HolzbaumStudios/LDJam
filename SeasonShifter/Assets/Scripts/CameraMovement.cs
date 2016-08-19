using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {

    private Transform target;
    private Vector3 m_CurrentVelocity;
    private float posX;
    private float posY;
    public float speed = 1.2f; //movement speed of the camera

    //How many unity the player can walk before the camera starts to follow
    public float horizontalRange = 2.5f;
    public float bottomRange = 0.1f;
    public float topRange = 3;
    private bool moveX = false;
    private bool moveY = false;

    // Use this for initialization
    void Start () {
        target = GameObject.Find("Player").transform;
        posX = target.position.x;
        posY = target.position.y;
        transform.position = new Vector3(posX, posY, transform.position.z);
        
	}
	
	// Update is called once per frame
	void Update () {
        //Draw lines
       /* Debug.DrawLine(transform.position + new Vector3(-horizontalRange, -0.75f, 0), transform.position + new Vector3(horizontalRange, -0.75f, 0), Color.green, 1, false);
        Debug.DrawLine(transform.position + new Vector3(-horizontalRange, bottomRange - 0.75f, 0), transform.position + new Vector3(-horizontalRange, topRange-0.75f, 0), Color.green, 1, false);
        Debug.DrawLine(transform.position + new Vector3(horizontalRange, bottomRange - 0.75f, 0), transform.position + new Vector3(horizontalRange, topRange -0.75f, 0), Color.green, 1, false);
        */

        //Check if character crosses lines
        if(target.position.x < transform.position.x - horizontalRange || target.position.x > transform.position.x + horizontalRange)
        {
            Debug.Log("Player crossed horizontal range!");
            moveX = true;
        }
        if(target.position.y > transform.position.y + topRange || target.position.y < transform.position.y - bottomRange)
        {
            Debug.Log("Player crossed vertical range!");
            moveY = true;
        }


        //If one of the bools is set to true, the camera shoud move
        if (moveX || moveY)
        {
            if (moveX)
                posX = target.position.x;
            else
                posX = transform.position.x;

            if (moveY)
                posY = target.position.y;
            else
                posY = transform.position.y;
            Vector3 targetPosition = new Vector3(posX, posY, -3);
            Vector3 newPos = Vector3.SmoothDamp(transform.position, targetPosition, ref m_CurrentVelocity, speed);
            transform.position = newPos;

            //Check if movement should be disabled
            if (moveX && target.position.x > transform.position.x - 0.2f && target.position.x < transform.position.x + 0.2f)
                moveX = false;
            if (moveY && target.position.y > transform.position.y - 0.05f && target.position.y < transform.position.y + 0.2f)
                moveY = false;
        }
    }

}
