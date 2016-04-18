using System.Collections;
using UnityEngine;

public class BridgeMovement : MonoBehaviour {


    // Variables for Movement
    private Vector2 originPosition;
    public bool movement;
    public float maxDistanceUp;
    public float maxDistanceDown;
    public float maxDistanceRight;
    public float maxDistanceLeft;
    private float movementSpeed;

    // Variables to check if maxDistance has been reached -> Changes movement direction
    public bool sideways = false;
    public bool movingdown;
    public bool movingright;

    // Use this for initialization
    void Start ()
    {
        originPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (movement == true)
        {
            if (sideways == false) // Wenn die Seitwärtsbewegung ausgeschaltet ist
            {
                float moveY = 1.0f;
                if (movingdown == true) moveY *= -1;
                movementSpeed = 2.0f;
                transform.Translate(new Vector2(0, moveY) * movementSpeed * Time.deltaTime);
                if (originPosition.y - maxDistanceDown > transform.position.y) movingdown = false;
                if (originPosition.y + maxDistanceUp < transform.position.y) movingdown = true;
            }
            else //Wenn Seitwärts true -> Bewegung links-rechts
            {
                float moveX = 1.0f;
                if (movingright == false) moveX *= -1;
                movementSpeed = 2.0f;
                transform.Translate(new Vector2(moveX, 0) * movementSpeed * Time.deltaTime);
                if (originPosition.x - maxDistanceLeft > transform.position.x) movingright = true;
                if (originPosition.x + maxDistanceRight < transform.position.x) movingright = false;
            }
        }
	}

}
