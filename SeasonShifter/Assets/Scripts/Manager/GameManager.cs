using UnityEngine;
using System.Collections;


public class GameManager : MonoBehaviour {
    
    public static GameManager gameManagerInstance = null; //Variable to make sure, there is always only one entity of the game manager
    public static SeasonManager seasonManagerInstance = null; //static variable for the season manager

    public GameObject player;
    public GameObject[] seasonObject;

    void Awake()
    {
        //Game Manager
        if (gameManagerInstance == null)
        {
            gameManagerInstance = this;
        }
        else if(gameManagerInstance != this)
        {
            Destroy(gameObject); //Make sure there is only one object
        }

        //Season Manager
        if(seasonManagerInstance == null)
        {
            if (!this.gameObject.GetComponent<SeasonManager>())
                seasonManagerInstance = this.gameObject.AddComponent<SeasonManager>();
            else
                seasonManagerInstance = this.gameObject.GetComponent<SeasonManager>();
        }
        seasonManagerInstance.SetGameObjects(seasonObject);
              
            
    }

}
