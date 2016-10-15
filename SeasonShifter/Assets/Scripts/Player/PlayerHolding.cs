using UnityEngine;
using System.Collections;

public class PlayerHolding : MonoBehaviour {

    /// <summary>
    /// This script manages the player holding on to objects like boxes and the firecone.
    /// When the item variable is not null, the player can't take/ hold any other items.
    /// 
    /// This script also manages letting go of objects.
    /// </summary>

    private GameObject itemHolding = null;

    void Start()
    {
        itemHolding = null;
    }

    public void Update()
    {
        if(isHolding())
        {
            if (Input.GetKeyDown(KeyCode.LeftAlt))
            {
                DropItem();
            }
        }
    }

    public void PickUpItem(GameObject item, bool holdingUp = true)
    {
        if (itemHolding == null)
            itemHolding = item;
        if (holdingUp)
            GetComponent<Animator>().SetBool("HoldingUp", holdingUp);
        else
            GetComponent<Animator>().SetBool("GrabingBox", true);

    }

    public void DropItem()
    {
        if (itemHolding != null)
        {
            itemHolding.SendMessage("DropItem");
            itemHolding = null;
            Animator animator = GetComponent<Animator>();
            animator.SetBool("HoldingUp", false);
            animator.SetBool("GrabingBox", false);
        }

    }

    public bool isHolding()
    {
        if (itemHolding == null)
        {
            return false;
        }
        return true;
    }

}
