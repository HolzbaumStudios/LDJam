using UnityEngine;
using System.Collections;

public class ChangeGameSpeed : MonoBehaviour {

    [Range(0.01f,1)]
    public float speed = 1;
    Rigidbody2D playerRigidbody;

	// Use this for initialization
	void Start () {
        Time.timeScale = speed;
	}

    void Update()
    {
        if (Time.timeScale != speed) Time.timeScale = speed;
        Log();
    }

    void Log()
    {
        if (playerRigidbody == null)
           playerRigidbody =  GameObject.FindWithTag("Player").GetComponent<Rigidbody2D>();

        Debug.Log(playerRigidbody.velocity.y);
    }
	
}
