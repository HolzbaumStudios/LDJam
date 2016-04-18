using UnityEngine;
using System.Collections;

public class loadLevelDoor : MonoBehaviour
{
    //Variables
    public string mapToLoad;
    private float movementSpeed = 1.5f;
    bool walkToWaypoint = true;
    public Transform walkPosition;
    public GameObject halfDoor;
    public GameObject fadeImage;
    public bool SetSavePoint = false;

    void OnTriggerStay2D(Collider2D player)
    {
        player.GetComponent<PlayerInput>().enabled = false;
        Rigidbody2D rigidbody = player.transform.GetComponent<Rigidbody2D>();
        rigidbody.velocity = Vector2.zero;

        if (walkToWaypoint)
        {
            rigidbody.position = Vector2.Lerp(rigidbody.position, walkPosition.position, Time.deltaTime * movementSpeed);
            if (walkPosition.position.x - 0.1f < rigidbody.position.x && walkPosition.position.x + 0.1f > rigidbody.position.x)
            {
                walkToWaypoint = false;
            }
        }
        else
        {
            halfDoor.SetActive(true);
            Vector2 targetPosition = transform.position + new Vector3(0.19f, 0, 0);
            rigidbody.position = Vector2.Lerp(rigidbody.position, targetPosition, Time.deltaTime * movementSpeed * 0.5f);
            if (transform.position.x - 0.1f < rigidbody.position.x && transform.position.x + 0.1f > rigidbody.position.x)
            {
                StartCoroutine(EndLevel());
            }
        }
    }


    IEnumerator EndLevel()
    {
        
        yield return new WaitForSeconds(0.2f);
        fadeImage.SetActive(true);
        if (!SetSavePoint)
        {
            PlayerPrefs.SetInt("SavedLevel", PlayerPrefs.GetInt("SavedLevel") + 1);
            SetSavePoint = true;
        }
        yield return new WaitForSeconds(1.1f);
        Application.LoadLevel(mapToLoad);
    }
}