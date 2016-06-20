using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEditor;

public static class TileEditor_SaveLoad {


    //public static TileEditor_BrushCollection savedBrushCollection;
    public static List<TileEditor_Brush> savedBrushList;


    public static void Save()
    {
        savedBrushList = TileEditor_BrushCollection.brushContainer;
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/brushCollection.tef"); //tef = tile editor file
        bf.Serialize(file, TileEditor_SaveLoad.savedBrushList);
        file.Close();
    }

    public static void SaveSingleBrush()
    {
        TileEditor_Brush savedBrush = TileEditor_BrushCollection.GetActiveBrush();
        BinaryFormatter bf = new BinaryFormatter();
        string path = EditorUtility.OpenFolderPanel("Select a folder", "", "");
        FileStream file = File.Create(path + "/" + savedBrush.brushName + ".teb"); //tef = tile editor brush
        bf.Serialize(file, savedBrush);
        file.Close();
    }

    public static void LoadSingleBrush()
    {
        BinaryFormatter bf = new BinaryFormatter();
        string path = EditorUtility.OpenFilePanel("Select a brush file", "", "teb");
        FileStream file = File.Open(path, FileMode.Open);
        TileEditor_Brush tempBrush =(TileEditor_Brush)bf.Deserialize(file);
        tempBrush.ConvertToSprite();
        file.Close();
        TileEditor_BrushCollection.AddBrush(tempBrush);
        TileEditor_BrushCollection.SaveBrushCollection();

    }

    public static void LoadBrushCollection()
    {
        if (File.Exists(Application.persistentDataPath + "/brushCollection.tef"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/brushCollection.tef", FileMode.Open);
            TileEditor_SaveLoad.savedBrushList = (List<TileEditor_Brush>)bf.Deserialize(file);
            file.Close();
            TileEditor_BrushCollection.brushContainer = savedBrushList;
        }
        else
        {
            Debug.Log("Savefile doesn't exist");
        }
    }


    /*
    public static void Save(TileEditor_ObjectHandler objectHandler)
    {
        savedObjectHandler.Add(objectHandler);
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/savedEditorData.tef"); //tef = tile editor file
        bf.Serialize(file, TileEditor_SaveLoad.savedObjectHandler);
        file.Close();
    }

    public static void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/savedEditorData.tef"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/savedEditorData.tef", FileMode.Open);
            TileEditor_SaveLoad.savedObjectHandler = (List<TileEditor_ObjectHandler>)bf.Deserialize(file);
            file.Close();
        }
    }*/
}
