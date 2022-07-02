using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveLoad
{
    static SaveData saveData; // object to hold the data we want saved

    static string filePath = Application.persistentDataPath + "/playersave.dat";


    // should be called when the player dies, or reaches a checkpoint
    public static void Save(){

        using (FileStream stream = new FileStream(filePath, FileMode.Create))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            saveData = new SaveData();
            formatter.Serialize(stream, saveData);
        }
    }

    public static void Load(){

        if (!File.Exists(filePath))
        {
            Debug.LogError("Save file couldn't be found!");
            return;
        }

        using (FileStream stream = new FileStream(filePath, FileMode.Open))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            saveData = (SaveData)formatter.Deserialize(stream); 
                                                                

            saveData.FinishLoad();  // sets the variables in game to the data that we've loaded from the file. 
        }
    }

}
