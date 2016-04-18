using UnityEngine;
using System.Collections;

public class MusicManager : MonoBehaviour {
    //Variables
    GameObject musicManager;
    public GameObject musicSummer;
    public GameObject musicWinter;
    ChangeSeason seasonManager;
    public enum Season { spring, summer, fall, winter };
    Season currentSeason;


    // Use this for initialization
    void Start () {
        seasonManager = GameObject.Find("GameManager").GetComponent<ChangeSeason>();
    }
	
	// Update is called once per frame
	void Update () {

        int seasonNumber = seasonManager.GetSeason();
        if ((Season)seasonNumber != currentSeason)
        {
            currentSeason = (Season)seasonNumber;
            SwitchMusic();
        }
	}

    public void SwitchMusic()
    {
        switch (currentSeason)
        {
            case Season.summer: this.musicSummer.GetComponent<AudioSource>().mute = false;
            this.musicWinter.GetComponent<AudioSource>().mute = true; break;
            case Season.winter: this.musicWinter.GetComponent<AudioSource>().mute = false;
                this.musicSummer.GetComponent<AudioSource>().mute = true; break;
        }
    }


}
