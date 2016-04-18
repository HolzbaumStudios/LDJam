using UnityEngine;
using System.Collections;

public class MenuGui : MonoBehaviour {

	
    void Start()
    {
        //Disable continue if no save game
        if (PlayerPrefs.GetInt("SavedLevel") < 1) transform.FindChild("Continue").gameObject.SetActive(false);
    }

    public void NewGame()
    {
        ResetSaveGame();
        LoadLevel();
    }

    public void Continue()
    {
        LoadLevel();
    }

    public void Quit()
    {
        Application.Quit();
    }

    void LoadLevel()
    {
        int savedLevel = PlayerPrefs.GetInt("SavedLevel"); //Whicht level the player has arrived
        if (savedLevel < 1) { savedLevel = 1; PlayerPrefs.SetInt("SavedLevel", savedLevel); }
        string savedLevelString = savedLevel.ToString(); //save the levelnumber as a string
        if (savedLevel < 10) savedLevelString = "0" + savedLevelString;
        string loadLevelName = "Map" + savedLevelString;

        Application.LoadLevel(loadLevelName);
    }
	
    void ResetSaveGame()
    {
        PlayerPrefs.SetInt("SavedLevel", 1);
        PlayerPrefs.SetInt("StaffEnabled", 0);
    }

}
