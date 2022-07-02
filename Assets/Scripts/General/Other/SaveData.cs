using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveData : MonoBehaviour
{
    Vector3 currentCheckpoint;
    //int currentAmmo;
    //bool[] itemsObtained;

    public SaveData(){

        currentCheckpoint = CheckpointHandler.currentSavedSpawn;
    }

    // called at the end of the load function. sets the variables to the ones loaded from file.
    public void FinishLoad(){

        CheckpointHandler.currentSavedSpawn = currentCheckpoint;

        Debug.Log("Loaded file.");
    }

}
