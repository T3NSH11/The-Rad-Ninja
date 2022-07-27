using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointHandler : MonoBehaviour
{
    public static GameObject player;

    public static Vector3 currentSavedSpawn = Vector3.zero;
    static Vector3 defaultSpawnpoint; // meant to be set to the player's position at the start of the level

    static bool autosaveEnabled = false;

    static CheckpointHandler handler;

    void Awake(){

        if (handler == null)
            handler = this;
        else
            Destroy(this);
    }

    void Start(){

        player = GameObject.Find("Player");
        defaultSpawnpoint = player.transform.position;

        //Respawn();  // respawn should be called when the scene is loaded
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.K))
        {
            Respawn();
        }
    }

    // should be called when entering a playable scene.
    public static void Respawn(){

        if (currentSavedSpawn == Vector3.zero)
            currentSavedSpawn = defaultSpawnpoint;

        player.transform.position = currentSavedSpawn;
        
    }

    public static void SetRespawn(Vector3 checkpointPos){

        currentSavedSpawn = checkpointPos;

        if(autosaveEnabled)
            SaveLoad.Save(); // save (including current checkpoint) when a checkpoint is reached.
    }

    // version of the function that allows you to check if the autosave 
    public static void SetRespawn(Vector3 checkpointPos, bool saveEnabled){

        currentSavedSpawn = checkpointPos;

        if (autosaveEnabled)
            SaveLoad.Save(); 
    }

}
