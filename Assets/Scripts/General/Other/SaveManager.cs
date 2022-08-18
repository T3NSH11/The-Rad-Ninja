using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// class that handles the use of the SaveLoad class for the given number of saves.
public class SaveManager : MonoBehaviour
{

    public SaveData saveSlot1, saveSlot2, saveSlot3;


    // call save with the given data and directory.
    public void SaveToSlot(SaveData data, Vector3 checkpoint, float health, string filePath) // test version of save func that allows for assigning save values.
    {
        data = new SaveData(checkpoint, health);
        SaveLoad.Save(data, filePath);
        Debug.Log("Saving with " + data);
    }


    public void SaveToSlot(SaveData data, string filePath) // version of save that takes the current data (used in final game).
    {
        data = new SaveData();
        SaveLoad.Save(data, filePath);
    }

    public void LoadSlot(SaveData dataToWriteTo, string filePath)
    {
        SaveLoad.Load(dataToWriteTo, filePath);
        //dataToWriteTo.FinishLoad(); // set the in-game variables to the given data. 
        //PlayerHealth.currentHealth = dataToWriteTo.currentHealth;
        Debug.Log("Loaded " + dataToWriteTo);
    }



    // functions for saving different data to different paths.
    public void SaveToSlot1() { SaveToSlot(saveSlot1, SaveLoad.filePath1); Debug.Log("Saving to file 1."); }
    public void SaveToSlot2() { SaveToSlot(saveSlot2, SaveLoad.filePath2); }
    public void SaveToSlot3() { SaveToSlot(saveSlot3, SaveLoad.filePath3); }

    public void LoadSlot1() { LoadSlot(saveSlot1, SaveLoad.filePath1); }
    public void LoadSlot2() { LoadSlot(saveSlot2, SaveLoad.filePath2); }
    public void LoadSlot3() { LoadSlot(saveSlot3, SaveLoad.filePath3); }


    /*public void ExecuteSave1()
    {
        saveSlot1 = new SaveData(save1Checkpoint, save1Health);
        SaveLoad.Save(saveSlot1, SaveLoad.filePath1);
    }

    public void LoadSlot1()
    {
        SaveLoad.Load(saveSlot1, SaveLoad.filePath1);
        saveSlot1.FinishLoad(); // set the in-game variables to the given data.
    }*/

}
