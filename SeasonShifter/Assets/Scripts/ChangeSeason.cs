﻿using UnityEngine;
using System.Collections;

public class ChangeSeason : MonoBehaviour {

    public enum Season { spring, summer, fall, winter};
    Season currentSeason = Season.summer;

    GameObject summerObject;
    GameObject winterObject;

    public Transform originObject;
    public GameObject changeSeasonEffect;

    bool changeAllowed = true;

    void Start()
    {
        summerObject = GameObject.FindGameObjectWithTag("Summer");
        winterObject = GameObject.FindGameObjectWithTag("Winter");
        if (currentSeason == Season.winter) { summerObject.SetActive(false); } else { winterObject.SetActive(false); }
        if (PlayerPrefs.GetInt("StaffEnabled") != 1) changeAllowed = false;
    }

    // Update is called once per frame
    void Update () {
	    if(Input.GetButtonDown("Fire1") && changeAllowed)
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

    public int GetSeason()
    {
        return (int)currentSeason;
    }


    public void AllowChange(bool allowed)
    {
        changeAllowed = allowed;
    }
}
