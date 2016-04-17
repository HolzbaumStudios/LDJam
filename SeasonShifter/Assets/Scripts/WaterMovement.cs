using UnityEngine;
using System.Collections;

public class WaterMovement : MonoBehaviour {

    private Vector2 originPosition;
    private float originX;
    private float originY;
    private float movementSpeed = 0.04f;
    public bool moveDown = true;
    public bool moveLeft = true;
    private float maxDistance = 0.03f;

	// Use this for initialization
	void Start () {
        originPosition = transform.position;
        originX = originPosition.x;
        originY = originPosition.y;
	}
	
	// Update is called once per frame
	void Update () {
        float xValue = 1;
        float yValue = 1;
        if (moveDown) yValue *= -1;
        if (moveLeft) xValue *= -1;

        transform.Translate(new Vector2(xValue, yValue) * movementSpeed * Time.deltaTime);

        if (moveDown && transform.position.y < originY - maxDistance) moveDown = false;
        if (!moveDown && transform.position.y > originY + maxDistance) moveDown = true;
        if (moveLeft && transform.position.x < originX - maxDistance) moveLeft = false;
        if (!moveLeft && transform.position.x > originX + maxDistance) moveLeft = true;

    }

}
