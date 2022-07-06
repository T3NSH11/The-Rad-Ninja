using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointTrigger : MonoBehaviour
{
    Vector3 checkpointPosition;
    bool hasBeenUsed = false;

    // sets the spawn point itself to the first child obj
    void Start(){
        checkpointPosition = gameObject.transform.GetChild(0).position;
    }

    void OnTriggerEnter(Collider collision){

        if (collision.CompareTag("Player") && !hasBeenUsed)
        {
            CheckpointHandler.SetRespawn(checkpointPosition);
            hasBeenUsed = true;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(checkpointPosition, 1f);
    }
}
