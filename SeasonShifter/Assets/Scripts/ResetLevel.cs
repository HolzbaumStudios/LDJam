using UnityEngine;
using System.Collections;

public class ResetLevel : MonoBehaviour {
    //Variables
    GameObject player;
    public float playerPosX;
    public float playerPosY;

    // Use this for initialization
    void Start () {
        player = GameObject.Find("Player");
        playerPosX = player.transform.position.x;
        Debug.Log("Das ist die x Position: " + playerPosX);
        playerPosY = player.transform.position.y;
        Debug.Log("Das ist die y Position: " + playerPosY);
    }
	
	// Update is called once per frame
	void Update () {
        
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        player.transform.position = new Vector2 (playerPosX, playerPosY);
    }
}
