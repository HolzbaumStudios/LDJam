using UnityEngine;
using System.Collections;


public class GameProgress : MonoBehaviour {

    ///////////////////////////////////////////////
    //This script takes care of the game progress//
    ///////////////////////////////////////////////

    public enum SaveSlots { NONE, SAVE1, SAVE2, SAVE3 }
    private SaveSlots activeSlot = SaveSlots.NONE;

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
