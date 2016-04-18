using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ChangeBackground : MonoBehaviour {

    public enum Season { spring, summer, fall, winter };
    Season currentSeason;
    ChangeSeason seasonManager;
    Image spriteRenderer;
    public Sprite[] backgroundImage;

    // Use this for initialization
    void Start () {
        seasonManager = GameObject.Find("GameManager").GetComponent<ChangeSeason>();
        spriteRenderer = GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update () {
        int seasonNumber = seasonManager.GetSeason();
        if (currentSeason != (Season)seasonNumber)
        {
            currentSeason = (Season)seasonNumber;
            ChangeSprite();
        }
    }

    void ChangeSprite()
    {
        switch(currentSeason)
        {
            case Season.spring: spriteRenderer.sprite = backgroundImage[0]; break;
            case Season.summer: spriteRenderer.sprite = backgroundImage[1]; break;
            case Season.fall: spriteRenderer.sprite = backgroundImage[2]; break;
            case Season.winter: spriteRenderer.sprite = backgroundImage[3]; break;
        }

    }
}
