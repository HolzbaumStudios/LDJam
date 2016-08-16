using UnityEngine;
using System.Collections;
using System;
using UnityStandardAssets._2D;

public class LevelManager : MonoBehaviour {
    
    public static LevelManager levelManagerInstance = null; //Variable to make sure, there is always only one entity of the game manager
    public static SeasonManager seasonManagerInstance = null; //static variable for the season manager

    public GameObject backgroundPrefab; //prefab slot
    private GameObject mainCamera;
    private GameObject player;

    void Awake()
    {
        //Game Manager
        if (levelManagerInstance == null)
        {
            levelManagerInstance = this;
        }
        else if(levelManagerInstance != this)
        {
            Destroy(gameObject); //Make sure there is only one object
        }

        //Season Manager
        if (seasonManagerInstance == null)
        {
            if (!this.gameObject.GetComponent<SeasonManager>())
                seasonManagerInstance = this.gameObject.AddComponent<SeasonManager>();
            else
                seasonManagerInstance = this.gameObject.GetComponent<SeasonManager>();
        }

        //Player
        player = GameObject.FindGameObjectWithTag("Player");

        //Camera
        Camera2DFollow cameraScript;
        mainCamera = Camera.main.gameObject;
       /* if (!mainCamera.GetComponent<Camera2DFollow>())
        {
            cameraScript = mainCamera.AddComponent<Camera2DFollow>();
            cameraScript.target = player.transform;
        }*/

        //Create the level background
        GameObject background;
        if (!GameObject.Find("BackgroundCanvas") && backgroundPrefab != null)
        {
            background = Instantiate(backgroundPrefab) as GameObject;
            background.GetComponent<Canvas>().worldCamera = Camera.main;
        }
        else
            Debug.LogWarning("No background created. Either it's already present or there is no prefab assigned");
    }

}
