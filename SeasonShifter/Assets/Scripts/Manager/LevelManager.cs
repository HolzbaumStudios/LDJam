using UnityEngine;
using System.Collections;
using System;
using UnityStandardAssets._2D;

public class LevelManager : MonoBehaviour {
    
    public static LevelManager levelManagerInstance = null; //Variable to make sure, there is always only one entity of the game manager
    public static SeasonManager seasonManagerInstance = null; //static variable for the season manager

    public GameObject backgroundPrefab; //prefab slot
    public GameObject SM_ChangeEffect;
    public AudioClip SM_ChangeSound;
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

        //Player
        player = GameObject.FindGameObjectWithTag("Player");

        //Season Manager
        if (seasonManagerInstance == null)
        {
            if (!this.gameObject.GetComponent<SeasonManager>())
                seasonManagerInstance = this.gameObject.AddComponent<SeasonManager>();
            else
                seasonManagerInstance = this.gameObject.GetComponent<SeasonManager>();
        }
        seasonManagerInstance.SetUpManager(player.GetComponent<PlayerMovement>().rightHand, SM_ChangeEffect, SM_ChangeSound); //Sets the origin of the change season effect

        //Camera
        CameraMovement cameraScript;
        mainCamera = Camera.main.gameObject;
        if(!mainCamera.GetComponent<CameraMovement>())
        {
            cameraScript = mainCamera.AddComponent<CameraMovement>();
        }

        //Create the level background
        GameObject background;
        if (!GameObject.Find("BackgroundCanvas") && backgroundPrefab != null)
        {
            background = Instantiate(backgroundPrefab) as GameObject;
            background.GetComponent<Canvas>().worldCamera = Camera.main;
        }
        else
            Debug.LogWarning("No background created. Either it's already present or there is no prefab assigned");

        //Add audio source
        if (!GetComponent<AudioSource>())
            this.gameObject.AddComponent<AudioSource>();
    }

}
