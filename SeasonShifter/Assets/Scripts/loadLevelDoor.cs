using UnityEngine;
using System.Collections;

public class loadLevelDoor : MonoBehaviour
{
    //Variables
    public string mapToLoad;

    void OnTriggerEnter2D(Collider2D player)
    {
        Application.LoadLevel(mapToLoad);
    }
}