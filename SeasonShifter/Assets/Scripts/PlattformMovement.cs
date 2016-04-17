using System.Collections;
using UnityEngine;

public class PlattformMovement : MonoBehaviour {

    // Variables
    private Vector2 originPosition;
    public bool movingdown; //to check if the plattform reached maxDistance -> Switch in the opposing direction
    private float movementSpeed;
    private float maxDistance = 2.0f;
    private ChangeSeason seasonManager;

    //Season Sprites
    public Sprite summersprite;
    public Sprite wintersprite;
    public Sprite fallsprite;
    public Sprite springsprite;

    enum Season { spring, summer, fall, winter };
    Season currentSeason = Season.summer;

    // Use this for initialization
    void Start ()
    {
        originPosition = transform.position;
        seasonManager = GameObject.Find("GameManager").GetComponent<ChangeSeason>();
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
        float moveY = 1.0f;
        if (movingdown == true) moveY *= -1;
        if (currentSeason == Season.summer)
        {
            movementSpeed = 2.0f;
            transform.Translate(new Vector2(0, moveY) * movementSpeed * Time.deltaTime);
            if (originPosition.y - maxDistance > transform.position.y) movingdown = false;
            if (originPosition.y + maxDistance < transform.position.y) movingdown = true;
        }
        else if (currentSeason == Season.winter)
        {
            movementSpeed = 0.0f;
            transform.Translate(new Vector2(0, moveY) * movementSpeed * Time.deltaTime);
            if (originPosition.y - maxDistance > transform.position.y) movingdown = false;
            if (originPosition.y + maxDistance < transform.position.y) movingdown = true;
        }
	}

    //Funktionen
    public void ChangeSprite()
    {
        switch (currentSeason)
        {
            case Season.summer: GetComponent<SpriteRenderer>().sprite = summersprite; break;
            case Season.winter: GetComponent<SpriteRenderer>().sprite = wintersprite; break;
            case Season.fall: GetComponent<SpriteRenderer>().sprite = fallsprite; break;
            case Season.spring: GetComponent<SpriteRenderer>().sprite = springsprite; break;
            default: break; 
        }
    }
}
