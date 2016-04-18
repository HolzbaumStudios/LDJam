using UnityEngine;
using System.Collections;

public class StaffScript : MonoBehaviour {

    bool staffEnabled = false;
    SpriteRenderer staffRenderer;
    public enum Season { spring, summer, fall, winter };
    Season currentSeason;
    ChangeSeason seasonManager;
    public Sprite winterStaff;
    public Sprite summerStaff;

    // Use this for initialization
    void Start () {
        staffRenderer = GetComponent<SpriteRenderer>();
        seasonManager = GameObject.Find("GameManager").GetComponent<ChangeSeason>();
        if(PlayerPrefs.GetInt("StaffEnabled") == 1)
        {
            staffEnabled = true;
            staffRenderer.enabled = true;
            int seasonNumber = seasonManager.GetSeason();
            currentSeason = (Season)seasonNumber;
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
        int seasonNumber = seasonManager.GetSeason();
        if((Season)seasonNumber != currentSeason)
        {
            currentSeason = (Season)seasonNumber;
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
            case Season.summer: staffRenderer.sprite = summerStaff; break;
            case Season.winter: staffRenderer.sprite = winterStaff; break;
        }
    }
}
