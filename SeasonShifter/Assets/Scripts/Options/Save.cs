using UnityEngine;
using System.Collections;

public class Save : MonoBehaviour {

    public const string SAVE_PATH = "/SAVES/";
    public const string SAVE_FILE_1 = "Shift1.sav";
    public const string SAVE_FILE_2 = "Shift2.sav";
    public const string SAVE_FILE_3 = "Shift3.sav";

    public void Save(int saveState)
    {
        string path = GetPath();
        string fileName = GetFileName(1);
        Debug.Log(completePath);
    }

    /**
    * Return the path of the save folder, without the save file
    *
    */
    public string GetPath()
    {
        return Application.dataPath + SAVE_PATH + file;
    }

    /**
    * Return the save file, without the path
    *
    */
    public string GetFileName(int saveState)
    {
        string file;
        switch (saveState)
        {
            case 1:
                file = SAVE_FILE_1;
                break;
            case 2:
                file = SAVE_FILE_2;
                break;
            case 3:
                file = SAVE_FILE_3;
                break;
            default:
                Debug.Log("Save state '" + saveState + "' is non-existent.");
        }
        return file;
    }
}
