using UnityEngine;
using System.Collections;

public class StaffScript : MonoBehaviour {

    /// <summary>
    /// Script is attached to the right hand of the player
    /// </summary>

    bool staffEnabled = false;
    SpriteRenderer staffRenderer;
    SeasonManager seasonManager;
    GameProgress gameProgress;
    public Sprite winterStaff;
    public Sprite summerStaff;
    public Sprite umbrella;

    // Use this for initialization
    void Start () {
        gameProgress = GameManager.gameProgressInstance;
        staffRenderer = GetComponent<SpriteRenderer>();
        seasonManager = GameObject.Find("LevelManager").GetComponent<SeasonManager>();
        seasonManager.CHANGE_SEASON += this.SeasonChanged;
        if (gameProgress.winterSeason)
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
