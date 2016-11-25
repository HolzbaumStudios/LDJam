using UnityEngine;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.SceneManagement;

public class SaveHandler : MonoBehaviour {

    private GameProgress[] saveObject = new GameProgress[3];

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
        Debug.Log("Loading Winter: " + saveObject[saveState - 1].winterSeason);
        GameManager.gameProgressInstance = saveObject[saveState - 1];
        
        GameManager.gameProgressInstance.SetSaveSlot(saveState);
        //LoadLevel(); //Still not implemented correctly
    }

    //This method should load the level after getting the save state. Functionality has to be defined.
    private void LoadLevel()
    {
       SceneManager.LoadScene(9);
    }

    /**
    * Load all the save states into temp objects
    *
    */
	private void LoadAllSaves()
	{
        for (int i = 1; i <= 3; i++)
        {
            string filePath = SaveUtil.GetFullPath(i);
            if(SaveUtil.IsFileExisting(filePath))
            {
                //Load the save to the temp objects
                BinaryFormatter bf = new BinaryFormatter();
                using (FileStream file = File.Open(filePath, FileMode.Open))
                {
                    ProgressData data = (ProgressData)bf.Deserialize(file);
                    Debug.Log("Obj Winter: " + data.winterSeason);
                    Debug.Log("Obj Spring: " + data.springSeason);
                    saveObject[i-1] = GetProgressData(data);
                }
            }
        }       
    }

    /**
    * Save the current game progress into the save file.
    * This method can be called without initializing the class
    *
    */
    public static void Save()
    {
        if (GameManager.gameProgressInstance != null)
        {
            GameProgress saveObject = GameManager.gameProgressInstance;
            BinaryFormatter bf = new BinaryFormatter();
            try
            {
                //Check if directory exists
                if (!SaveUtil.IsDirectoryExisting())
                {
                    SaveUtil.CreateSaveDirectory();
                }
                //Get Filename
                string fileName = SaveUtil.GetFullPath(saveObject.GetSaveSlot());
                ProgressData progress = SetProgressData(saveObject);
                Debug.Log("Sav Winter: " + progress.winterSeason);
                Debug.Log("Sav Spring: " + progress.springSeason);
                using (FileStream file = File.Create(fileName))
                {
                    bf.Serialize(file, progress);
                }
            }
            catch(IOException ex)
            {
                Debug.LogError("Can't save progress: " + ex);
            }
        }
        else
        {
            Debug.LogError("Game can't be saved: No gameProgress Instance");
        }
    }

    private static ProgressData SetProgressData(GameProgress currentGameProgress)
    {
        ProgressData progressData = new ProgressData();
        progressData.fallSeason = currentGameProgress.fallSeason;
        progressData.winterSeason = currentGameProgress.winterSeason;
        progressData.springSeason = currentGameProgress.springSeason;

        progressData.umbrella = currentGameProgress.umbrella;
        return progressData;
    }

    private GameProgress GetProgressData(ProgressData loadedData)
    {
        GameProgress progress = new GameProgress();
        progress.fallSeason = loadedData.fallSeason;
        progress.winterSeason = loadedData.winterSeason;
        progress.springSeason = loadedData.springSeason;

        progress.umbrella = loadedData.umbrella;
        return progress;
    }
}
