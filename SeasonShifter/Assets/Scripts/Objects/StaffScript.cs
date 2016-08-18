using UnityEngine;
using System.Collections;

public class StaffScript : MonoBehaviour {

    bool staffEnabled = false;
    SpriteRenderer staffRenderer;
    SeasonManager seasonManager;
    public Sprite winterStaff;
    public Sprite summerStaff;

    // Use this for initialization
    void Start () {
        staffRenderer = GetComponent<SpriteRenderer>();
        seasonManager = GameObject.Find("LevelManager").GetComponent<SeasonManager>();
        seasonManager.CHANGE_SEASON += this.SeasonChanged;
        if (PlayerPrefs.GetInt("StaffEnabled") == 1)
        {
            staffEnabled = true;
            staffRenderer.enabled = true;
            ChangeSprite();
        }
        else
        {
            staffRenderer.enabled = false;
        }
	}
	

    public void EnableStaff()
    {
        staffRenderer.enabled = true;
        staffEnabled = true;
    }

    void ChangeSprite()
    {
        switch(seasonManager.currentSeason)
        {
            case SeasonManager.Season.summer: staffRenderer.sprite = summerStaff; break;
            case SeasonManager.Season.winter: staffRenderer.sprite = winterStaff; break;
        }
    }

    private void SeasonChanged(object source)
    {
        ChangeSprite();
    }
}
