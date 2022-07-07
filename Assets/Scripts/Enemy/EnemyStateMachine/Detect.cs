using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detect : EnemyState
{
    float Detectiontimer;
    public override void StartState(EnemyManager manager)
    {
        Detectiontimer = manager.WanderDetectionRequired;
    }

    public override void UpdateState(EnemyManager manager)
    {
        if (manager.FOV.PlayerDetected)
        {
            manager.gameObject.transform.LookAt(manager.gameObject.transform.position);
            Detectiontimer -= Time.deltaTime;
        }
        else
        {
            manager.LastPlayerLoc = manager.Player.transform.position;
            manager.SwitchState(manager.SearchForPlayer);
        }

        if(Detectiontimer <= 0)
        {
            manager.SwitchState(manager.ChasePlayer);
        }

    }
}
