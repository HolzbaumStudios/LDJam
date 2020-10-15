using UnityEngine;
using System.Collections;

public class LightingScript : MonoBehaviour {

    public bool caveIsLeft = false;


	void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            if ((col.transform.position.x > this.transform.position.x && !caveIsLeft) || col.transform.position.x < this.transform.position.x && caveIsLeft)
            {
                PassedTrigger(true, col.gameObject);
            }
            else
            {
                PassedTrigger(false, col.gameObject);
            }
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(this.transform.position - new Vector3(0,1.5f), this.transform.position + new Vector3(0, 1.5f));
    }

    void PassedTrigger(bool entered, GameObject player)
    {
        SpriteRenderer darkLayer = player.transform.Find("LightingLayer").GetComponent<SpriteRenderer>();
        if (GameProgress.gameProgressInstance.winterSeason)
        {
            Light staffLight = player.GetComponent<PlayerMovement>().rightHand.Find("SpotLight").GetComponent<Light>();
            staffLight.enabled = entered;
        }
        Animator animator = darkLayer.gameObject.GetComponent<Animator>();
        animator.SetBool("FadeIn", entered);
        if (entered)
        {
            animator.enabled = entered;
            darkLayer.enabled = entered;
        }
        else
            StartCoroutine(DisableAnimator(animator, darkLayer));
    }

    IEnumerator DisableAnimator(Animator animator, SpriteRenderer renderer)
    {
        yield return new WaitForSeconds(1.8f);
        renderer.enabled = false;
        animator.enabled = false;
    }
}
