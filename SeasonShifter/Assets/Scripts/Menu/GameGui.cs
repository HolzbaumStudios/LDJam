using UnityEngine;
using System.Collections;

public class GameGui : MonoBehaviour {

    public GameObject pausePanel;
    private bool paused = false;
    private ChangeSeason seasonManager;

    void Start()
    {
        seasonManager = GameObject.Find("GameManager").GetComponent<ChangeSeason>();
    }

	// Update is called once per frame
	void Update () {
	    if(Input.GetButtonDown("Cancel"))
        {
            SetPause();
        }
	}

    public void SetPause()
    {
        if(paused)
        {
            paused = false;
            pausePanel.SetActive(false); //disable gui
            //disable movement
            Time.timeScale = 1;
            seasonManager.AllowChange(true);
        }
        else
        {
            paused = true;
            pausePanel.SetActive(true); //enable gui
            //disable movement
            Time.timeScale = 0;
            seasonManager.AllowChange(false);
        }
    }

    public void Exit(string levelName)
    {
        Time.timeScale = 1;
        Application.LoadLevel(levelName);
    }
}
