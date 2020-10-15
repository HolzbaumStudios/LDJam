using UnityEngine;
using System.Collections;

public class PickUpScript_Old : MonoBehaviour {


    string objectName;
    Inventory inventory;
    public string color = "green";
    //GameObject musicObjects = GameObject.Find("MusicManager").transform.FindChild("MusicObjects").gameObject;
    GameObject musicObjects;

    void Start()
    {
        objectName = this.gameObject.transform.name;
        musicObjects = GameObject.Find("MusicManager").transform.Find("MusicObjects").gameObject;
        inventory = GameObject.Find("GameManager").GetComponent<Inventory>();
        if (objectName == "PickStaff" && PlayerPrefs.GetInt("StaffEnabled") == 1)
        {
            Destroy(this.gameObject);
        }
        else if (objectName == "UpgradeUmbrella" && PlayerPrefs.GetInt("GlidingAllowed")==1)
        {
            Destroy(this.gameObject);
        }

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (objectName == "PickStaff")
        {
            //Shoe Achievement Text
            GameObject.Find("GUI").transform.Find("InfoText").gameObject.SetActive(true);
            //Find Music Manager for ObjectSound
            musicObjects.GetComponent<AudioSource>().Play();
            //Change Season
            ChangeSeason seasonManager = GameObject.Find("GameManager").GetComponent<ChangeSeason>();
            seasonManager.SeasonChange();
            seasonManager.AllowChange(true);
            GameObject.Find("staff").GetComponent<StaffScript>().EnableStaff();
            PlayerPrefs.SetInt("StaffEnabled",1);
            Destroy(this.gameObject);
        }
        else if(objectName == "UpgradeUmbrella")
        {
            col.gameObject.GetComponent<PlayerInput>().AllowGliding();
            musicObjects.GetComponent<AudioSource>().Play();
            GameObject.Find("GUI").transform.Find("InfoText").gameObject.SetActive(true);
            Destroy(this.gameObject);
        }
        else if((objectName == "KeyGreen" || objectName == "KeyBlue"))
        {
            if(Application.loadedLevelName == "Map02")GameObject.Find("GUI").transform.Find("InfoText").gameObject.SetActive(true);
            musicObjects.GetComponent<AudioSource>().Play();
            inventory.KeyCollected(color);
            Destroy(this.gameObject);
        }
    }
}
