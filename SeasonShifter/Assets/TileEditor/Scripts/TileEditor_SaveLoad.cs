using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

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
