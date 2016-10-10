using UnityEngine;
using System.Collections;

public class Mushroom : MonoBehaviour {

    private SeasonManager seasonManager;
    public Sprite[] mushroomSprites;
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
        seasonManager = GameObject.Find("LevelManager").GetComponent<SeasonManager>();
        seasonManager.CHANGE_SEASON += this.SeasonChanged;
    }


    void ChangeSprite()
    {
        switch (seasonManager.currentSeason)
        {
            case SeasonManager.Season.spring: spriteRenderer.sprite = mushroomSprites[0]; boxCollider.enabled = false; break;
            case SeasonManager.Season.summer: spriteRenderer.sprite = mushroomSprites[1]; boxCollider.enabled = false; break;
            case SeasonManager.Season.fall: spriteRenderer.sprite = mushroomSprites[2]; boxCollider.enabled = true; break;
            case SeasonManager.Season.winter: spriteRenderer.sprite = null; boxCollider.enabled = false; break;
        }
    }


     private void SeasonChanged(object source)
    {
        ChangeSprite();
    }

    public void StartAnimation()
    {
        var animator = GetComponent<Animator>();
        animator.SetTrigger("MushroomJump");
    }
}
