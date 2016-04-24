using UnityEngine;
using System.Collections;

public class RaiseFlag : MonoBehaviour
{
    public bool flagTouched = false;
    public Sprite raisedFlag;
    GameObject levelContainer;
    GameObject player;
    public float playerPosXnew;
    public float playerPosYnew;

    // Use this for initialization
    void Start()
    {
        player = GameObject.Find("Player");
        levelContainer = GameObject.Find("LevelContainer");
    }


    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player") && !flagTouched)
        {
            flagTouched = true;
            transform.parent.GetComponent<SpriteRenderer>().sprite = raisedFlag;
            playerPosXnew = player.transform.position.x;
            //Debug.Log("Das ist die neue x Position: " + playerPosXnew);
            playerPosYnew = player.transform.position.y;
            //Debug.Log("Das ist die neue y Position: " + playerPosYnew);
            // Set new player position
            levelContainer.GetComponent<ResetLevel>().playerPosX = playerPosXnew;
            levelContainer.GetComponent<ResetLevel>().playerPosY = playerPosYnew;
        }
    }
}
