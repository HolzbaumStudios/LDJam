using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {

    Transform player;
    private float posX;
    private float posY;
    private float speed = 1.2f;

    //How many unity the player can walk before the camera starts to follow
    private float rangeX = 4;
    private float rangeY = 3;
    private bool moveCamera = false;

    // Use this for initialization
    void Start () {
        player = GameObject.Find("Player").transform;
        posX = player.position.x;
        posY = player.position.y;
        transform.position = new Vector3(posX, posY, transform.position.z);
        
	}
	
	// Update is called once per frame
	void Update () {
       
        //Move Camera x
        float currentCamX = transform.position.x;
        float currentCamY = transform.position.y;
        float currentCamZ = transform.position.z;


        if (!moveCamera)
        {
            if (player.position.x > currentCamX + rangeX || player.position.x < currentCamX - rangeX)
            {
                moveCamera = true;
            }
            //Move Camera y
            if (player.position.y > currentCamY + rangeY || player.position.y < currentCamY - rangeY)
            {
                moveCamera = true;
            }
        }
        else
        {
            Vector3 targetVector3 = new Vector3(player.position.x, player.position.y, currentCamZ);
            if(!(player.position.x < currentCamX + 0.5f && player.position.x > currentCamX - 0.5f) && !(player.position.y < currentCamY + 0.5f && player.position.y > currentCamY - 0.5f))
            {
                transform.position = Vector3.Slerp(transform.position, targetVector3, speed * Time.deltaTime);
            }
            else
            {
                moveCamera = false;
            }
        }

    }
}
