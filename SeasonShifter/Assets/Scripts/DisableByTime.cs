using UnityEngine;
using System.Collections;

public class DisableByTime : MonoBehaviour {

    //This script waits the set number of seconds until it disables the object it is attached to
    public float secondsToDisable;

    void Start()
    {
        StartCoroutine(DisableObject());
    }

    IEnumerator DisableObject()
    {
        yield return new WaitForSeconds(secondsToDisable);
        this.gameObject.SetActive(false);
    }
}
