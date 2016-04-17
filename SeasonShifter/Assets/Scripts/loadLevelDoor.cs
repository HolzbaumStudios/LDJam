using UnityEngine;
using System.Collections;

public class loadLevelDoor : MonoBehaviour
{
    //Variables
    public string mapToLoad;

    void OnTriggerEnter(Collider player)
    {
        Application.LoadLevel(mapToLoad);
    }
}