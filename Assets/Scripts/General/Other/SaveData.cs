using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public PlayerHealth health;
    SerializableVector3 currentCheckpoint;
    public float currentHealth;
    float currentMana;
    //int currentAmmo;
    //bool[] itemsObtained;

    public SaveData(){
        currentCheckpoint = new SerializableVector3();
        currentCheckpoint.x = CheckpointHandler.currentSavedSpawn.x;
        currentCheckpoint.y = CheckpointHandler.currentSavedSpawn.y;
        currentCheckpoint.z = CheckpointHandler.currentSavedSpawn.z; // need a separate class for representing vector3 in serializable form
        currentHealth = PlayerHealth.currentHealth;
    }

    public SaveData(Vector3 checkpoint, float health)
    {
        currentCheckpoint = new SerializableVector3();
        currentCheckpoint.x = checkpoint.x;
        currentCheckpoint.y = checkpoint.y;
        currentCheckpoint.z = checkpoint.z;
        currentHealth = health;
    }

    // called at the end of the load function. sets the variables to the ones loaded from file.
    public void FinishLoad(){

        //CheckpointHandler.currentSavedSpawn = currentCheckpoint;
        CheckpointHandler.SetRespawn(new Vector3(currentCheckpoint.x, 
                                                 currentCheckpoint.y, 
                                                 currentCheckpoint.z), false); // convert values back into a vector3.
        PlayerHealth.SetHealth(currentHealth);
    }

}

[System.Serializable]
class SerializableVector3
{
    public float x;
    public float y;
    public float z;
}
