using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveData : MonoBehaviour
{
    Vector3 currentCheckpoint;
    float currentHealth;
    float currentMana;
    //int currentAmmo;
    //bool[] itemsObtained;

    public SaveData(){

        currentCheckpoint = CheckpointHandler.currentSavedSpawn;
        currentHealth = PlayerHealth.currentHealth;
    }

    public SaveData(Vector3 checkpoint, float health)
    {
        currentCheckpoint = checkpoint;
        currentHealth = health;
    }

    // called at the end of the load function. sets the variables to the ones loaded from file.
    public void FinishLoad(){

        //CheckpointHandler.currentSavedSpawn = currentCheckpoint;
        CheckpointHandler.SetRespawn(currentCheckpoint, false);
        PlayerHealth.SetHealth(currentHealth);

        Debug.Log("Loaded file.");
    }

}
