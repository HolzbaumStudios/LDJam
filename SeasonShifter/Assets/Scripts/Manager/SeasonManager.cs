using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SeasonManager : MonoBehaviour {

    public enum Season { spring, summer, fall, winter };
    public Season currentSeason { get; private set; }
    private int tempSeasonId = 0; //Used for the guy

    public GameObject[] seasonObject; //0 = spring, 1 = summer, 2 = fall, 3 = winter;

    private Transform effectOrigin; //Where the change season effect is instantiated
    private GameObject changeEffect; //The effect to change the season
    private PlayerMovement playerMovement;
    private PlayerInput playerInput;
    private GameProgress gameProgress;

    //AUDIO COMPONENTS
    private AudioClip changeSound; //The sound when the season changes
    private AudioSource audioSource;

    //GUI COMPONENTS
    private GameObject seasonWheel; //Container for the season Wheel
    private GameObject[] seasonWheelComponents; //Container for the season components of the season wheel
    private bool wheelActivated = false;

    private Vector2 wheelMousePosition; //When the season wheel is active, the mouse position is checked for control of the wheel


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

        gameProgress = GameManager.gameProgressInstance;

        currentSeason = Season.summer;
        SetSeason();

        audioSource = GetComponent<AudioSource>();

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        playerMovement = player.GetComponent<PlayerMovement>();
        playerInput = player.GetComponent<PlayerInput>();

        //Get GUI Objects
        seasonWheel = GameObject.Find("GameGUI").transform.FindChild("SeasonWheel").gameObject;
        seasonWheelComponents = new GameObject[4];
        seasonWheelComponents[0] = seasonWheel.transform.FindChild("SpringWheel").gameObject;
        seasonWheelComponents[1] = seasonWheel.transform.FindChild("SummerWheel").gameObject;
        seasonWheelComponents[2] = seasonWheel.transform.FindChild("FallWheel").gameObject;
        seasonWheelComponents[3] = seasonWheel.transform.FindChild("WinterWheel").gameObject;
        seasonWheel.SetActive(false);
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1") && playerMovement.GetGroundedState() && !playerMovement.GetSwimmingState() && !wheelActivated && gameProgress.winterSeason)
        {
            OpenWheel();
            seasonWheel.SetActive(true);
            tempSeasonId = (int)currentSeason;
            wheelActivated = true;
            wheelMousePosition = Input.mousePosition;
            playerInput.DisableInput(true);
        }

        if(wheelActivated)
        {
            //Check for changes with the key
            if (Input.GetAxis("Horizontal") > 0)
            {
                tempSeasonId = 1;
                wheelMousePosition = Input.mousePosition;
            }
            else if (Input.GetAxis("Horizontal") < 0)
            {
                tempSeasonId = 3;
                wheelMousePosition = Input.mousePosition;
            }
            else if (Input.GetAxis("Vertical") > 0 && gameProgress.springSeason)
            {
                tempSeasonId = 0;
                wheelMousePosition = Input.mousePosition;
            }
            else if (Input.GetAxis("Vertical") < 0 && gameProgress.winterSeason)
            {
                tempSeasonId = 2;
                wheelMousePosition = Input.mousePosition;
            }

            //Check for changes with the mouse
            if (Input.mousePosition.x > wheelMousePosition.x + 5)
            {
                tempSeasonId = 1;
                wheelMousePosition = Input.mousePosition;
            }
            else if (Input.mousePosition.x < wheelMousePosition.x - 5)
            {
                tempSeasonId = 3;
                wheelMousePosition = Input.mousePosition;
            }
            else if (Input.mousePosition.y > wheelMousePosition.y + 5 && gameProgress.springSeason)
            {
                tempSeasonId = 0;
                wheelMousePosition = Input.mousePosition;
            }
            else if (Input.mousePosition.y < wheelMousePosition.y - 5 && gameProgress.fallSeason)
            {
                tempSeasonId = 2;
                wheelMousePosition = Input.mousePosition;
            }

            //Change the GUI
            SetGuiWheel(tempSeasonId);
        }

        if (Input.GetButtonUp("Fire1") && wheelActivated)
        {
            seasonWheel.SetActive(false);
            if (tempSeasonId != (int)currentSeason)
                SeasonChange(tempSeasonId);
            wheelActivated = false;
            playerInput.DisableInput(false);
        }
    }

    //Event handler
    public delegate void SeasonHandler(object source);
    public event SeasonHandler CHANGE_SEASON;

    //Starts the change effect coroutine. This function is also called from other scripts
    public void SeasonChange(int id)
    {
        StartCoroutine(ChangeEffect(id));
    }

    IEnumerator ChangeEffect(int id)
    {
        audioSource.PlayOneShot(changeSound, 1);
        Vector3 instantiatePosition = new Vector3(effectOrigin.position.x, effectOrigin.position.y, 1);
        GameObject changeEffectObject = Instantiate(changeEffect, instantiatePosition, transform.rotation) as GameObject;
        yield return new WaitForSeconds(0.5f);
        switch(id)
        {
            case 0: currentSeason = Season.spring;
                break;
            case 1:
                currentSeason = Season.summer;
                break;
            case 2:
                currentSeason = Season.fall;
                break;
            case 3:
                currentSeason = Season.winter;
                break;
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

    //Set the selected season
    public void SetGuiWheel(int id)
    {
        Color baseColor = new Color32(111, 111, 111, 255);
        Color selectedColor = Color.white;

        foreach (GameObject wheelPart in seasonWheelComponents)
            wheelPart.GetComponent<Image>().color = baseColor;

        seasonWheelComponents[id].GetComponent<Image>().color = selectedColor;
    }

    //GetVariableInfos when opening the wheel
    public void OpenWheel()
    {
        if (gameProgress.springSeason && seasonWheelComponents[0].activeSelf == false)
            seasonWheelComponents[0].SetActive(true);
        if (gameProgress.fallSeason && seasonWheelComponents[2].activeSelf == false)
            seasonWheelComponents[2].SetActive(true);
    }
}
