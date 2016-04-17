using UnityEngine;
using System.Collections;

public class ChangeSeason : MonoBehaviour {

    enum Season { summer, winter};
    Season currentSeason = Season.winter;

    GameObject summerObject;
    GameObject winterObject;

    public Transform originObject;
    public GameObject changeSeasonEffect;

    void Start()
    {
        summerObject = GameObject.FindGameObjectWithTag("Summer");
        winterObject = GameObject.FindGameObjectWithTag("Winter");
        if (currentSeason == Season.winter) { summerObject.SetActive(false); } else { winterObject.SetActive(false); }
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
        Vector3 instantiatePosition = new Vector3(originObject.position.x, originObject.position.y, 1);
        GameObject changeEffect = Instantiate(changeSeasonEffect, instantiatePosition, transform.rotation) as GameObject;
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
