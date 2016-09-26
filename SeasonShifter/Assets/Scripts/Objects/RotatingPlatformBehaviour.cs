using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RotatingPlatformBehaviour : MonoBehaviour {

    //Component Variables
    Animator animator;

    // Variables for Movement
    public bool rotateLeft = false;
    public float movementSpeed = 1;


    // Variables for Seasons
    private SeasonManager seasonManager;

    //Season Sprites
    public Sprite[] standardSprite;
    public Sprite[] winterSprite;

    //Child objects
    private List<Transform> childObjects;


    // Use this for initialization
    void Start()
    {
        seasonManager = GameObject.Find("LevelManager").GetComponent<SeasonManager>();
        seasonManager.CHANGE_SEASON += this.SeasonChanged; //Subscribe to season changes

        animator = this.GetComponent<Animator>();

        childObjects = new List<Transform>();
        foreach (Transform child in transform)
            childObjects.Add(child);
    }

    // Update is called once per frame
    void Update()
    {
        if ((transform.localScale.x > 0 && rotateLeft) || transform.localScale.x < 0 && !rotateLeft)
        {
            Vector3 newScale = transform.localScale;
            newScale.x *= -1;
            transform.localScale = newScale;
        }
            
    }


    //Change Sprite of child object based on the season
    public void ChangeSprite()
    {
        switch (seasonManager.currentSeason)
        {
            case SeasonManager.Season.winter:
                {
                    animator.enabled = false;
                    for(int i=0; i<childObjects.Count; i++)
                    {
                        childObjects[i].GetComponent<SpriteRenderer>().sprite = winterSprite[i];
                    }
                }
                break;
            default:
                {
                    animator.enabled = true;
                    for (int i = 0; i < childObjects.Count; i++)
                    {
                        childObjects[i].GetComponent<SpriteRenderer>().sprite = standardSprite[i];
                    }
                }
                break;
        }
    }

    //Call function when event is raised
    private void SeasonChanged(object source)
    {
        ChangeSprite();
    }
}
