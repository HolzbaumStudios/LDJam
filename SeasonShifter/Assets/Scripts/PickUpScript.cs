using UnityEngine;
using System.Collections;

public class PickUpScript : MonoBehaviour {


    string objectName;
    Inventory inventory;

    void Start()
    {
        objectName = this.gameObject.transform.name;
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
            ChangeSeason seasonManager = GameObject.Find("GameManager").GetComponent<ChangeSeason>();
            seasonManager.SeasonChange();
            seasonManager.AllowChange(true);
            GameObject.Find("staff").GetComponent<StaffScript>().EnableStaff();
            PlayerPrefs.SetInt("StaffEnabled",1);
            Destroy(this.gameObject);
        }
        else if(objectName == "KeyGreen")
        {
            inventory.KeyCollected();
            Destroy(this.gameObject);
        }
    }
}
