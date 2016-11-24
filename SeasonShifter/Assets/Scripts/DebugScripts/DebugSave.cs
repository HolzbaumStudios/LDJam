using UnityEngine;
using System.Collections;

public class DebugSave : MonoBehaviour {

	public void SaveCurrentState()
    {
        SaveHandler.Save();
    }
}
