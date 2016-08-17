using UnityEngine;
using System.Collections;

public class MusicManager : MonoBehaviour {
    //Variables
    GameObject musicManager;
    public GameObject musicSummer;
    public GameObject musicWinter;
    public GameObject musicObjects;
    SeasonManager seasonManager;


    // Use this for initialization
    void Start () {
        seasonManager = GameObject.Find("LevelManager").GetComponent<SeasonManager>();
        //Subscribe to event handler
        seasonManager.CHANGE_SEASON += this.SeasonChanged;
    }
	

    public void SwitchMusic()
    {
        switch (seasonManager.currentSeason)
        {
            case SeasonManager.Season.summer: this.musicSummer.GetComponent<AudioSource>().mute = false;
            this.musicWinter.GetComponent<AudioSource>().mute = true; break;
            case SeasonManager.Season.winter: this.musicWinter.GetComponent<AudioSource>().mute = false;
            this.musicSummer.GetComponent<AudioSource>().mute = true; break;
        }
    }

    private void SeasonChanged(object source)
    {
        SwitchMusic();
    }


}
