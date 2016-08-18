using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {

    private Transform target;
    private Vector3 m_CurrentVelocity;
    private float posX;
    private float posY;
    public float speed = 1.2f; //movement speed of the camera

    //How many unity the player can walk before the camera starts to follow
    public float horizontalRange = 2;
    public float bottomRange = 0.1f;
    public float topRange = 2;
    private bool moveCamera = false;

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
        Debug.DrawLine(transform.position + new Vector3(-horizontalRange, -0.75f, 0), transform.position + new Vector3(horizontalRange, -0.75f, 0), Color.green, 80, false);
        Debug.DrawLine(transform.position + new Vector3(-horizontalRange, bottomRange - 0.75f, 0), transform.position + new Vector3(-horizontalRange, topRange-0.75f, 0), Color.green, 80, false);
        Debug.DrawLine(transform.position + new Vector3(horizontalRange, bottomRange - 0.75f, 0), transform.position + new Vector3(horizontalRange, topRange -0.75f, 0), Color.green, 80, false);

        //Check if character crosses lines
        if(target.position.x < transform.position.x - horizontalRange || target.position.x > transform.position.x + horizontalRange)
        {
            Debug.Log("Player crossed horizontal range!");
            moveCamera = true;
        }
        if(target.position.y > transform.position.y + topRange || target.position.y < transform.position.y - bottomRange)
        {
            Debug.Log("Player crossed vertical range!");
            moveCamera = true;
        }

        if (moveCamera)
        {
            Vector3 targetPosition = new Vector3(target.position.x, target.position.y, -3);
            Vector3 newPos = Vector3.SmoothDamp(transform.position, targetPosition, ref m_CurrentVelocity, speed);
            transform.position = newPos;
        }
    }

}
