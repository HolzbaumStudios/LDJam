using UnityEngine;
using System.Collections;

public class Inventory : MonoBehaviour {

    private bool keyCollected = false;

    public void KeyCollected()
    {
        keyCollected = true;
        GameObject.Find("GUI").transform.FindChild("KeyIcon").GetComponent<KeyScript>().KeyCollected();
    }

    public bool ReturnKeyvalue()
    {
        return keyCollected;
    }
}
