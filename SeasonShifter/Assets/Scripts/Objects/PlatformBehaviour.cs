using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlatformBehaviour : MonoBehaviour {

    // Variables for Movement
    private Vector2 originPosition;
    public bool movement;
    public bool drawGizmos;
    public float maxDistanceUp;
    public float maxDistanceDown;
    public float maxDistanceRight;
    public float maxDistanceLeft;
    public float movementSpeed = 2;
    private bool preventChange = false;
    private bool winterMovementEnabled = false;


    // Variables to check if maxDistance has been reached -> Changes movement direction
    public bool sideways = false;
    public bool movingdown;
    public bool movingright;

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
        originPosition = transform.position;
        seasonManager = GameObject.Find("LevelManager").GetComponent<SeasonManager>();
        seasonManager.CHANGE_SEASON += this.SeasonChanged; //Subscribe to season changes

        childObjects = new List<Transform>();
        foreach (Transform child in transform)
            childObjects.Add(child);
    }

    // Update is called once per frame
    void Update()
    {
        if (movement == true && (seasonManager.currentSeason != SeasonManager.Season.winter || winterMovementEnabled))
        {
            if (sideways == false) // Wenn die Seitwärtsbewegung ausgeschaltet ist
            {
                float moveY = 1.0f;
                if (movingdown == true) moveY *= -1;
                transform.Translate(new Vector2(0, moveY) * movementSpeed * Time.deltaTime);
                if (originPosition.y - maxDistanceDown > transform.position.y) movingdown = false;
                if (originPosition.y + maxDistanceUp < transform.position.y) movingdown = true;
            }
            else //Wenn Seitwärts true -> Bewegung links-rechts
            {
                float moveX = 1.0f;
                if (movingright == false) moveX *= -1;
                transform.Translate(new Vector2(moveX, 0) * movementSpeed * Time.deltaTime);
                if (originPosition.x - maxDistanceLeft > transform.position.x) movingright = true;
                if (originPosition.x + maxDistanceRight < transform.position.x) movingright = false;
            }
        }
    }

    void OnDrawGizmos()
    {
        if (drawGizmos)
        {
            //Horizontal Line
            Gizmos.DrawLine(transform.position - new Vector3(maxDistanceLeft, 0), transform.position + new Vector3(maxDistanceRight, 0));
            //Vertical Line
            Gizmos.DrawLine(transform.position - new Vector3(0, maxDistanceDown), transform.position + new Vector3(0, maxDistanceUp));
        }
    }

    //Change Sprite of child object based on the season
    public void ChangeSprite()
    {
        switch (seasonManager.currentSeason)
        {
            case SeasonManager.Season.winter:
                {
                    if (!preventChange) //detect if freezing is disabled
                    {
                        for (int i = 0; i < childObjects.Count; i++)
                        {
                            if (i == 0)
                                childObjects[i].GetComponent<SpriteRenderer>().sprite = winterSprite[0];
                            else if (i == childObjects.Count - 1)
                                childObjects[i].GetComponent<SpriteRenderer>().sprite = winterSprite[2];
                            else
                                childObjects[i].GetComponent<SpriteRenderer>().sprite = winterSprite[1];
                        }
                    }
                    else
                    {
                        winterMovementEnabled = true;
                    }
                }
                break;
            default:
                {
                    for (int i = 0; i < childObjects.Count; i++)
                    {
                        if (i == 0)
                            childObjects[i].GetComponent<SpriteRenderer>().sprite = standardSprite[0];
                        else if (i == childObjects.Count - 1)
                            childObjects[i].GetComponent<SpriteRenderer>().sprite = standardSprite[2];
                        else
                            childObjects[i].GetComponent<SpriteRenderer>().sprite = standardSprite[1];
                    }
                    winterMovementEnabled = false;
                }
                break;
        }
    }

    //Disables freezing of seasons
    public void DisableChange(bool disable)
    {
        preventChange = disable;
    }

    //Call function when event is raised
    private void SeasonChanged(object source)
    {
        ChangeSprite();
    }
}
