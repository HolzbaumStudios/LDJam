using UnityEngine;
using System.Collections;

public class ChangeSeason : MonoBehaviour {

    enum Season { summer, winter};
    Season currentSeason = Season.winter;

    GameObject summerObject;
    GameObject winterObject;


    public GameObject changeSeasonEffect;

    void Start()
    {
        summerObject = GameObject.FindGameObjectWithTag("Summer");
        winterObject = GameObject.FindGameObjectWithTag("Winter");
    }

    // Update is called once per frame
    void Update () {
	    if(Input.GetButtonDown("Fire1"))
        {
            StartCoroutine(ChangeEffect());
        }
	}


    IEnumerator ChangeEffect()
    {
        GameObject changeEffect = Instantiate(changeSeasonEffect, transform.position, transform.rotation) as GameObject;
        yield return new WaitForSeconds(0.9f);
        if (currentSeason == Season.summer)
        {
            currentSeason = Season.winter;
            summerObject.SetActive(false);
            winterObject.SetActive(true);
        }
        else
        {
            currentSeason = Season.summer;
            summerObject.SetActive(true);
            winterObject.SetActive(false);
        }
        yield return new WaitForSeconds(0.2f);
        Destroy(changeEffect);
    }
}
