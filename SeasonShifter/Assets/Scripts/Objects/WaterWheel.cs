using UnityEngine;
using System.Collections;

public class WaterWheel : MonoBehaviour {

    /// <summary>
    /// Stops the movement of the water wheel, when the season is set to winter
    /// </summary>

    private SeasonManager seasonManager;
    private Animator animator; 

    void Start()
    {
        seasonManager = GameObject.Find("LevelManager").GetComponent<SeasonManager>();
        //Subscribe to event handler
        seasonManager.CHANGE_SEASON += this.SeasonChanged;
        animator = this.GetComponent<Animator>();
    }

	private void StopAnimation()
    {
        if (seasonManager.currentSeason == SeasonManager.Season.winter)
            animator.enabled = false;
        else
            animator.enabled = true;
    }


    //Event is raised by Season Manager
    private void SeasonChanged(object source)
    {
        StopAnimation();
    }
}
