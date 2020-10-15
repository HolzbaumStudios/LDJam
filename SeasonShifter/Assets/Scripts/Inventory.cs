using UnityEngine;
using System.Collections;

public class Inventory : MonoBehaviour {

    private bool greenKeyCollected = false;
    private bool blueKeyCollected = false;

    public void KeyCollected(string color)
    {
        switch(color)
        {
            case "green": GameObject.Find("GUI").transform.Find("KeyGreenIcon").GetComponent<KeyScript>().KeyCollected(); greenKeyCollected = true; break;
            case "blue": GameObject.Find("GUI").transform.Find("KeyBlueIcon").GetComponent<KeyScript>().KeyCollected(); blueKeyCollected = true; break;
        }
        
    }

    public bool ReturnKeyvalue(string color)
    {
        switch (color)
        {
            case "green": return greenKeyCollected; break;
            case "blue": return blueKeyCollected; break;
            default: return false; break;
        }
    }
}
