using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveLoad
{
    static SaveData saveData; // object to hold the data we want saved

    static SaveData prevSave;
    static string prevSavePath;

    public static string filePath1 = Application.persistentDataPath + "/playersaveone.dat";
    public static string filePath2 = Application.persistentDataPath + "/playersavetwo.dat";
    public static string filePath3 = Application.persistentDataPath + "/playersavethree.dat"; // names for different file paths 


    // should be called when the player dies, or reaches a checkpoint
    public static void Save(){

        using (FileStream stream = new FileStream(filePath1, FileMode.Create)) // default to save 1.
        {
            BinaryFormatter formatter = new BinaryFormatter();
            saveData = new SaveData(); 
            formatter.Serialize(stream, saveData);
        }
    }

    public static void Save(SaveData givenSave, string saveTo)
    {

        using (FileStream stream = new FileStream(saveTo, FileMode.Create))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            saveData = givenSave;
            formatter.Serialize(stream, saveData);
        }

        prevSave = givenSave;
        prevSavePath = saveTo;
    }


    public static void Load(SaveData givenSave, string loadFrom){

        if (!File.Exists(loadFrom))
        {
            Debug.LogError("Save file couldn't be found!");
            return;
        }

        using (FileStream stream = new FileStream(loadFrom, FileMode.Open))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            givenSave = (SaveData)formatter.Deserialize(stream);

            //givenSave.FinishLoad();  // sets the variables in game to the data that we've loaded from the file. 
        }
    }

}
