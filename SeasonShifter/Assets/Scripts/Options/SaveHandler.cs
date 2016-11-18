using UnityEngine;
using System.Collections;

public class SaveHandler : MonoBehaviour {

    GameProgress[] saveObject = new GameProgress[3];

    public void Awake()
    {
        if (!SaveUtil.IsDirectoryExisting())
        {
            SaveUtil.CreateSaveDirectory();
        }
        LoadAllSaves();
    }

    /**
    *
    *This method set the GameProgress from the selected save slot as the loaded one
    */
	public void Load(int saveState)
	{
        string completePath = SaveUtil.GetPath() + SaveUtil.GetFileName(saveState);
        GameProgress selectedSave = saveObject[saveState - 1];
        if(selectedSave.GetSaveSlot() == 0)
        {
            selectedSave.SetSaveSlot(saveState);
        }
        GameManager.gameProgressInstance = selectedSave;
	}

	private void LoadAllSaves()
	{
        string path = SaveUtil.GetPath();
        for (int i = 1; i <= 3; i++)
        {
            string filePath = path + SaveUtil.GetFileName(i);
            saveObject[i - 1] = new GameProgress();
            if(SaveUtil.IsFileExisting(filePath))
            {
                //Load the save to the temp objects
            }
        }
	}
}
