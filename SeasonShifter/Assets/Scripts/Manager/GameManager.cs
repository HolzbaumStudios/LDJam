using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public static GameManager gameManagerInstance;

	public GameObject musicManagerPrefab;
    public GameObject gameProgressPrefab;

	// Use this for initialization
	void Awake () {
		//Game Manager Instance
		if (gameManagerInstance == null)
			gameManagerInstance = this;
		else if (gameManagerInstance != this)
			Destroy(gameObject); //Make sure there is only one object

        if (GameProgress.gameProgressInstance == null)
            Instantiate(gameProgressPrefab);

		//Set the game manger to undestroyable
		DontDestroyOnLoad(gameManagerInstance);

		//Create the music manager
		if(!GameObject.Find("MusicManager"))
		{
			GameObject MusicManager = Instantiate(musicManagerPrefab);
			MusicManager.name = musicManagerPrefab.name;
			DontDestroyOnLoad(MusicManager);
		}
	}
	
}
