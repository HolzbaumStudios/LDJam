using UnityEngine;
using System.Collections;

public class ResetLevel : MonoBehaviour {
    //Variables
    GameObject player;
    float playerPosX;
    float playerPosY;

    // Use this for initialization
    void Start () {
        player = GameObject.Find("Player");
        playerPosX = player.transform.position.x;
        playerPosY = player.transform.position.y;
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        player.transform.position = new Vector2 (playerPosX, playerPosY);
    }
}
