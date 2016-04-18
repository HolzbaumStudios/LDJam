using UnityEngine;
using System.Collections;

public class MenuMusicHandler : MonoBehaviour {

    public AudioSource source;

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
	
	// Update is called once per frame
	void OnLevelWasLoaded()
    {
        string level = Application.loadedLevelName;
        if (level == "Main" || level == "Credits" || level == "Controls" || level == "Story" || level == "EndScene")
        {
            if (!source.isPlaying) source.Play();
            source.mute = false;
        }
        else
        {
            source.mute = true;
        }
    }
}
