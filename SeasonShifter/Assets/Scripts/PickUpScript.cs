using UnityEngine;
using System.Collections;

public class PickUpScript : MonoBehaviour {


    string objectName;
    Inventory inventory;
    //GameObject musicObjects = GameObject.Find("MusicManager").transform.FindChild("MusicObjects").gameObject;
    GameObject musicObjects;

    void Start()
    {
        objectName = this.gameObject.transform.name;
        musicObjects = GameObject.Find("MusicManager").transform.FindChild("MusicObjects").gameObject;
        inventory = GameObject.Find("GameManager").GetComponent<Inventory>();
        if (objectName == "PickStaff" && PlayerPrefs.GetInt("StaffEnabled") == 1)
        {
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(objectName == "PickStaff")
        {
            Debug.Log("Touched staff!");
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
        else if(objectName == "KeyGreen")
        {
            musicObjects.GetComponent<AudioSource>().Play();
            inventory.KeyCollected();
            Destroy(this.gameObject);
        }
    }
}
