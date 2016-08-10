using UnityEngine;
using System.Collections;

public class StaffScript : MonoBehaviour {

    bool staffEnabled = false;
    SpriteRenderer staffRenderer;
    public enum Season { spring, summer, fall, winter };
    SeasonManager.Season currentSeason;
    SeasonManager seasonManager;
    public Sprite winterStaff;
    public Sprite summerStaff;

    // Use this for initialization
    void Start () {
        staffRenderer = GetComponent<SpriteRenderer>();
        seasonManager = GameObject.Find("GameManager").GetComponent<SeasonManager>();
        if(PlayerPrefs.GetInt("StaffEnabled") == 1)
        {
            staffEnabled = true;
            staffRenderer.enabled = true;
            
            currentSeason = seasonManager.currentSeason;
            ChangeSprite();
        }
        else
        {
            staffRenderer.enabled = false;
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(currentSeason != seasonManager.currentSeason)
        {
            currentSeason = seasonManager.currentSeason;
            ChangeSprite();
        }
    }

    public void EnableStaff()
    {
        staffRenderer.enabled = true;
        staffEnabled = true;
    }

    void ChangeSprite()
    {
        switch(currentSeason)
        {
            case SeasonManager.Season.summer: staffRenderer.sprite = summerStaff; break;
            case SeasonManager.Season.winter: staffRenderer.sprite = winterStaff; break;
        }
    }
}
