using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class TileEditor_SaveLoad {

    /*
    public static TileEditor_BrushCollection savedBrushCollection;

    public static void Save(TileEditor_ObjectHandler objectHandler)
    {
        savedObjectHandler.Add(objectHandler);
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/savedEditorData.tef"); //tef = tile editor file
        bf.Serialize(file, TileEditor_SaveLoad.savedObjectHandler);
        file.Close();
    }

    public static void Save(TileEditor_BrushCollection brushCollection)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/savedEditorData.tef"); //tef = tile editor file
        bf.Serialize(file, TileEditor_SaveLoad.savedBrushCollection);
        file.Close();
    }

    public static void LoadBrushCollection()
    {
        if (File.Exists(Application.persistentDataPath + "/savedEditorData.tef"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/savedEditorData.tef", FileMode.Open);
            TileEditor_SaveLoad.savedBrushCollection = (TileEditor_BrushCollection)bf.Deserialize(file);
            file.Close();
        }
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
