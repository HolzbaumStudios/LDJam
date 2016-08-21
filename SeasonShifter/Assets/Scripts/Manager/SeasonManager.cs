using UnityEngine;
using System.Collections;

public class SeasonManager : MonoBehaviour {

    public enum Season { spring, summer, fall, winter };
    public Season currentSeason { get; private set; }

    public GameObject[] seasonObject; //0 = spring, 1 = summer, 2 = fall, 3 = winter;

    private Transform effectOrigin; //Where the change season effect is instantiated
    private GameObject changeEffect; //The effect to change the season
    private AudioClip changeSound; //The sound when the season changes
    private AudioSource audioSource;
    private PlayerMovement playerMovement;

    void Start()
    {
        //Get the season objects
        seasonObject = new GameObject[4];
        if(GameObject.Find("Spring"))
            seasonObject[0] = GameObject.Find("Spring");
        if (GameObject.Find("Summer"))
            seasonObject[1] = GameObject.Find("Summer");
        if (GameObject.Find("Fall"))
            seasonObject[2] = GameObject.Find("Fall");
        if (GameObject.Find("Winter"))
            seasonObject[3] = GameObject.Find("Winter");

        currentSeason = Season.summer;
        SetSeason();

        audioSource = GetComponent<AudioSource>();

        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1") && playerMovement.GetGroundedState() && !playerMovement.GetSwimmingState())
        {
            SeasonChange();
        }
    }

    //Event handler
    public delegate void SeasonHandler(object source);
    public event SeasonHandler CHANGE_SEASON;

    //Starts the change effect coroutine. This function is also called from other scripts
    public void SeasonChange()
    {
        StartCoroutine(ChangeEffect());
    }

    IEnumerator ChangeEffect()
    {
        audioSource.PlayOneShot(changeSound, 1);
        Vector3 instantiatePosition = new Vector3(effectOrigin.position.x, effectOrigin.position.y, 1);
        GameObject changeEffectObject = Instantiate(changeEffect, instantiatePosition, transform.rotation) as GameObject;
        yield return new WaitForSeconds(0.5f);
        if (currentSeason == Season.summer)
        {
            currentSeason = Season.winter;
        }
        else
        {
            currentSeason = Season.summer;
        }
        SetSeason();
        CHANGE_SEASON(this);//Call event, that season has changed
        yield return new WaitForSeconds(0.2f);
        Destroy(changeEffectObject);
    }


    //Set the current season
    public void SetSeason()
    {
        foreach(GameObject season in seasonObject)
        {
            if(season != null)
                season.SetActive(false);
        }
        seasonObject[(int)currentSeason].SetActive(true);
    }

    //Sets the season object variable. The objects are defined in the game manager
    public void SetGameObjects(GameObject[] objects)
    {
        seasonObject = new GameObject[objects.Length];
        for(int i=0; i<objects.Length; i++)
        {
            seasonObject[i] = objects[i];
        }
    }

    //Sets the origin of the change season effect -> is called by the LevelManager
    public void SetUpManager(Transform origin, GameObject changeEffect, AudioClip changeSound)
    {
        effectOrigin = origin;
        this.changeEffect = changeEffect;
        this.changeSound = changeSound;
    }

}
