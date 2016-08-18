using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ChangeBackground : MonoBehaviour {

    SeasonManager seasonManager;
    Image spriteRenderer;
    public Sprite[] backgroundImage;



    // Use this for initialization
    void Start () {
        seasonManager = GameObject.Find("LevelManager").GetComponent<SeasonManager>();
        //Subscribe to event handler
        seasonManager.CHANGE_SEASON += this.SeasonChanged;
        spriteRenderer = GetComponent<Image>();
	}

    void ChangeSprite()
    {
        switch(seasonManager.currentSeason)
        {
            case SeasonManager.Season.spring: spriteRenderer.sprite = backgroundImage[0]; break;
            case SeasonManager.Season.summer: spriteRenderer.sprite = backgroundImage[1]; break;
            case SeasonManager.Season.fall: spriteRenderer.sprite = backgroundImage[2]; break;
            case SeasonManager.Season.winter: spriteRenderer.sprite = backgroundImage[3]; break;
        }
    }

    private void SeasonChanged(object source)
    {
        ChangeSprite();
    }
}
