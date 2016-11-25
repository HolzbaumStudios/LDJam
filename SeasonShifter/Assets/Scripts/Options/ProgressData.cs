using UnityEngine;
using System;
using System.Collections;

[System.Serializable]
public class ProgressData {

    //Activated Seasons
    [SerializeField]
    public bool winterSeason;
    public bool springSeason;
    public bool fallSeason;

    //Activated Abilities
    public bool umbrella;
}
