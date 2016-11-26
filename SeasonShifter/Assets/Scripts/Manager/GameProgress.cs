﻿using UnityEngine;
using System.Collections;

[System.Serializable]
public class GameProgress : MonoBehaviour {

    ///////////////////////////////////////////////
    //This script takes care of the game progress//
    ///////////////////////////////////////////////

    public static GameProgress gameProgressInstance;

    void Awake()
    {
        //Game Progress Instance
        if (gameProgressInstance == null)
            gameProgressInstance = this;
        else if (gameProgressInstance != this)
            Destroy(gameObject); //Make sure there is only one object

        DontDestroyOnLoad(this.gameObject);
    }


    public enum SaveSlots { NONE, SAVE1, SAVE2, SAVE3 }
    private SaveSlots activeSlot = SaveSlots.NONE;

    //--------------SAVE VARIABLES------------------------

    //Activated Seasons
    public bool winterSeason = false;
    public bool springSeason = false;
    public bool fallSeason = false;

    //Activated Abilities
    public bool umbrella = false;
    


    //----------------Methods-----------------
    public int GetSaveSlot()
    {
        return (int)activeSlot;
    }

    public void SetSaveSlot(int id)
    {
        activeSlot = (SaveSlots)id;
    }

}
