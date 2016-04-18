﻿using System.Collections;
using UnityEngine;

public class PlattformMovement : MonoBehaviour {

    // Variables
    private Vector2 originPosition;
    public float maxDistance;
    //private float maxDistance = 2.0f;
    public bool sideways = false;
    public bool movingdown; //to check if the plattform reached maxDistance -> Switch in the opposing direction
    public bool movingright; //to check if the plattform reached maxDistance -> Switch in the opposing direction
    private float movementSpeed;
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
        if (sideways == false) // Wenn die Seitwärtsbewegung ausgeschaltet ist
        {
            int seasonNumber = seasonManager.GetSeason();
            if ((Season)seasonNumber != currentSeason)
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
        else //Wenn Seitwärts true -> Bewegung links-rechts
        {
            int seasonNumber = seasonManager.GetSeason();
            if ((Season)seasonNumber != currentSeason)
            {
                currentSeason = (Season)seasonNumber;
                ChangeSprite();
            }
            float moveX = 1.0f;
            if (movingright == false) moveX *= -1;
            if (currentSeason == Season.summer)
            {
                movementSpeed = 2.0f;
                transform.Translate(new Vector2(moveX, 0) * movementSpeed * Time.deltaTime);
                if (originPosition.x - maxDistance > transform.position.x) movingright = true;
                if (originPosition.x + maxDistance < transform.position.x) movingright = false;
            }
            else if (currentSeason == Season.winter)
            {
                movementSpeed = 0.0f;
                transform.Translate(new Vector2(moveX, 0) * movementSpeed * Time.deltaTime);
                if (originPosition.x - maxDistance > transform.position.x) movingright = true;
                if (originPosition.x + maxDistance < transform.position.x) movingright = false;
            }
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
