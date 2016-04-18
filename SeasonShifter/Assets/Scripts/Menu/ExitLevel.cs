using UnityEngine;
using System.Collections;

public class ExitLevel : MonoBehaviour {

    public string levelName;

	// Update is called once per frame
	void Update () {
	    if(Input.GetButtonDown("Cancel"))
        {
            EscapeScene();
        }
	}

    public void EscapeScene()
    {
        Application.LoadLevel(levelName);
    }
}
