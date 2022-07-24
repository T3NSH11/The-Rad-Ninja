using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasePlayer : EnemyState
{
    public override void StartState(EnemyManager manager)
    {
        manager.mDesiredAnimationSpeed = 1f;
    }

    public override void UpdateState(EnemyManager manager)
    {
        if(manager.FOV.PlayerDetected == true)
        {
            //manager.gameObject.transform.position += (manager.Player.transform.position - manager.transform.position).normalized * manager.RunSpeed * Time.deltaTime;
            manager.transform.LookAt(new Vector3(manager.Player.transform.position.x, manager.transform.position.y, manager.Player.transform.position.x));
        }
        else
        {
            manager.LastPlayerLoc = manager.Player.transform.position;
            manager.SwitchState(manager.SearchForPlayer);
        }

        if (Vector3.Distance(manager.transform.position, manager.Player.transform.position) < manager.AttackRange)
        {
            manager.SwitchState(manager.Attack);
        }
    }
}
