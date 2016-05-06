using UnityEngine;
using System.Collections;

public class ChangeGameSpeed : MonoBehaviour {

    [Range(0.01f,1)]
    public float speed = 1;

	// Use this for initialization
	void Start () {
        Time.timeScale = speed;
	}

    void Update()
    {
        if (Time.timeScale != speed) Time.timeScale = speed;
    }
	
}
