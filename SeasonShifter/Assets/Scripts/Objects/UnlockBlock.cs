using UnityEngine;
using System.Collections;

public class UnlockBlock : MonoBehaviour {

    private Inventory inventory;

	// Use this for initialization
	void Start () {
        inventory = GameObject.Find("GameManager").GetComponent<Inventory>();
	}
	
	void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.CompareTag("Player") && inventory.ReturnKeyvalue())
        {
            Destroy(this.gameObject);
        }
    }
}
