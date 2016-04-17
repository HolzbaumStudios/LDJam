using UnityEngine;
using System.Collections;

public class WaterCollider : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            Debug.Log("Swimming");
            col.GetComponent<PlayerMovement>().ChangeSwimmingState(true);
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            Debug.Log("Not Swimming");
            col.GetComponent<PlayerMovement>().ChangeSwimmingState(false);
        }
    }
}
