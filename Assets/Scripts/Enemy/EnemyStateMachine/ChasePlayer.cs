using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasePlayer : EnemyState
{
    public override void StartState(EnemyManager manager)
    {

    }

    public override void UpdateState(EnemyManager manager)
    {
        if(manager.FOV.PlayerDetected == true)
        {
            //enemy moves towards player
        }
        else
        {
            manager.SwitchState(manager.SearchForPlayer);
        }
    }
}
