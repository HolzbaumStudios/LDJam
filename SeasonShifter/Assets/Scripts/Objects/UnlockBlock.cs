using UnityEngine;
using System.Collections;

public class UnlockBlock : MonoBehaviour {

    private Inventory inventory;
    public string color = "green";

	// Use this for initialization
	void Start () {
        inventory = GameObject.Find("GameManager").GetComponent<Inventory>();
	}
	
	void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.CompareTag("Player") && inventory.ReturnKeyvalue(color))
        {
            StartCoroutine(DestroyObject());
        }
    }

    IEnumerator DestroyObject()
    {
        GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(0.8f);
        Destroy(this.gameObject);
    }
}
