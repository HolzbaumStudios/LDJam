using UnityEngine;
using System.Collections;
using System;
using System.IO;

public static class SaveUtil
{

    public const string SAVE_PATH = "/saves/";
    public static readonly string[] SAVE_FILES = { "Shift1.sav", "Shift2.sav", "Shift3.sav" };


    /**
    * Return the path of the save folder, without the save file
    *
    */
    public static string GetPath()
    {
        return Application.dataPath + SAVE_PATH;
    }

    /**
    * Return the save file, without the path
    *
    */
    public static string GetFileName(int saveState)
    {
        string fileName;
        try
        {
            fileName = SAVE_FILES[saveState - 1];
        }
        catch (IndexOutOfRangeException)
        {
            fileName = "";
            Debug.LogError("Save State " + saveState + " doesn't exist.");
        }
        return fileName;
    }

    /**
    * Return the save file with the full patzh
    *
    */
    public static string GetFullPath(int saveState)
    {
        return GetPath() + GetFileName(saveState);
    }

    public static bool IsFileExisting(string filePath)
    {
        return File.Exists(filePath);
    }

    public static bool IsDirectoryExisting()
    {
        return Directory.Exists(Application.dataPath + SAVE_PATH);
    }

    public static void CreateSaveFile(string filePath)
    {
        try
        {
            File.Create(filePath);
        }
        catch (System.IO.IOException ex)
        {
            Debug.LogError("Could not create file: " + ex);
        }
    }

    public static void CreateSaveDirectory()
    {
        Directory.CreateDirectory(Application.dataPath + SAVE_PATH);
    }

}
