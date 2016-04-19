using UnityEngine;
using System.Collections;

public class ShowMessage : MonoBehaviour {

    // Scripts
    void Start()
    {
        this.transform.FindChild("InfoTextStart").gameObject.SetActive(true);
    }
}
