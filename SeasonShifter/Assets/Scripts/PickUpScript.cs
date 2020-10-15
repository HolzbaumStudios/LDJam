using UnityEngine;
using System.Collections;

public class PickUpScript : MonoBehaviour {


    private string objectName;
    private GameProgress gameProgress;
    private Inventory inventory;
    private GameObject musicObjects;

    void Start()
    {
        objectName = this.gameObject.transform.name;
        gameProgress = GameProgress.gameProgressInstance;
        musicObjects = MusicManager.musicManagerInstance.musicObjects;
        inventory = GameManager.gameManagerInstance.GetComponent<Inventory>();
        //Staff pickup at level 1-1
        if (objectName == "Staff" && gameProgress.winterSeason)
        {
            Destroy(this.gameObject);
        }
        //Umbrella pickup
        else if (objectName == "UpgradeUmbrella" && gameProgress.umbrella)
        {
            Destroy(this.gameObject);
        }

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (objectName == "Staff")
        {
            //Show Achievement Text
            //GameObject.Find("GUI").transform.FindChild("InfoText").gameObject.SetActive(true);
            //Find Music Manager for ObjectSound
            musicObjects.GetComponent<AudioSource>().Play();
            //Change Season
            SeasonManager seasonManager = LevelManager.seasonManagerInstance;
            seasonManager.SeasonChange(3);
            gameProgress.winterSeason = true; //Enables winter and season change
            GameObject.FindWithTag("Player").GetComponent<PlayerMovement>().rightHand.Find("staff").GetComponent<StaffScript>().EnableStaff();
            Light staffLight = col.GetComponent<PlayerMovement>().rightHand.Find("SpotLight").GetComponent<Light>();
            staffLight.enabled = true;
            Destroy(this.gameObject);
        }
        else if(objectName == "UpgradeUmbrella")
        {
            col.gameObject.GetComponent<PlayerInput>().AllowGliding();
            musicObjects.GetComponent<AudioSource>().Play();
            GameObject.Find("GUI").transform.Find("InfoText").gameObject.SetActive(true);
            Destroy(this.gameObject);
        }
    }
}
