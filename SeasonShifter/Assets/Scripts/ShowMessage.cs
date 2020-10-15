using UnityEngine;
using System.Collections;

public class ShowMessage : MonoBehaviour {

    // Scripts
    void Start()
    {
        this.transform.Find("InfoTextStart").gameObject.SetActive(true);
    }
}
