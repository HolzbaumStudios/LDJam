using UnityEngine;
using System.Collections;

public class MenuGui : MonoBehaviour {

    public GameObject fadeBlack;
	
    void Start()
    {
        //Disable continue if no save game
        if (PlayerPrefs.GetInt("SavedLevel") < 1) transform.FindChild("Continue").gameObject.SetActive(false);
    }

    public void NewGame()
    {
        ResetSaveGame();
        StartCoroutine(LoadLevel());
    }

    public void Continue()
    {
        StartCoroutine(LoadLevel());
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Credits()
    {
        Application.LoadLevel("Credits");
    }

    public void Controls()
    {
        Application.LoadLevel("Controls");
    }

    public void Story()
    {
        Application.LoadLevel("Story");
    }

    IEnumerator LoadLevel()
    {
        //Get - Set LevelNumber and name
        int savedLevel = PlayerPrefs.GetInt("SavedLevel"); //Whicht level the player has arrived
        if (savedLevel < 1) { savedLevel = 1; PlayerPrefs.SetInt("SavedLevel", savedLevel); }
        string savedLevelString = savedLevel.ToString(); //save the levelnumber as a string
        if (savedLevel < 10) savedLevelString = "0" + savedLevelString;
        string loadLevelName = "Map" + savedLevelString;
        //Fade music 
        GameObject menuMusic = GameObject.Find("MenuMusic");
        AudioSource menuMusicAudio = menuMusic.GetComponent<AudioSource>();
        Animator menuMusicAnimator = menuMusic.GetComponent<Animator>();
        menuMusicAnimator.SetBool("FadeMusic", true);
        //Fadescreen
        Instantiate(fadeBlack, transform.position, transform.rotation);

        yield return new WaitForSeconds(2);
        menuMusicAudio.Stop();
        menuMusicAudio.time = 0; //Sets track at the beginning
        menuMusicAnimator.SetBool("FadeMusic", false);
        Application.LoadLevel(loadLevelName);
    }
	
    void ResetSaveGame()
    {
        PlayerPrefs.SetInt("SavedLevel", 1);
        PlayerPrefs.SetInt("StaffEnabled", 0);
        PlayerPrefs.SetInt("GlidingAllowed", 0);
    }


}
