using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class KeyScript : MonoBehaviour {

    public bool keyAvailable = false;
    public Sprite greenKey;
    bool keyCollected = false;

	// Use this for initialization
	void Start () {
	    if(!keyAvailable)
        {
            this.gameObject.SetActive(false);
        }
	}
	
	public void KeyCollected()
    {
        keyCollected = true;
        Image keySprite = GetComponent<Image>();
        keySprite.sprite = greenKey;
        keySprite.color = new Color32(255, 255, 255, 255);
        
    }
}
