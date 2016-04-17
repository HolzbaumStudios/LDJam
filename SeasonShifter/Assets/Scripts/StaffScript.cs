using UnityEngine;
using System.Collections;

public class StaffScript : MonoBehaviour {

    bool staffEnabled = false;
    SpriteRenderer staffRenderer;

	// Use this for initialization
	void Start () {
        staffRenderer = GetComponent<SpriteRenderer>();
        if(PlayerPrefs.GetInt("StaffEnabled") == 1)
        {
            staffEnabled = true;
            staffRenderer.enabled = true;
        }
        else
        {
            staffRenderer.enabled = false;
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void EnableStaff()
    {
        staffRenderer.enabled = true;
        staffEnabled = true;
    }

}
