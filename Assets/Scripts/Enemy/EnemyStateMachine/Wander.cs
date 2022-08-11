using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wander : EnemyState
{
    public override void StartState(EnemyManager manager)
    {
        manager.mDesiredAnimationSpeed = 0.15f;
        manager.MyAnimator.SetBool("Moving", true);
    }

    public override void UpdateState(EnemyManager manager)
    {
        float node_Distance = Vector3.Distance(manager.current_SetPath.pathNodes[manager.currentPath_NodeID].position, manager.transform.position);
        //manager.transform.position = Vector3.MoveTowards(manager.transform.position, manager.current_SetPath.pathNodes[manager.currentPath_NodeID].position, Time.deltaTime * manager.WalkSpeed);

        var object_Rotation = Quaternion.LookRotation(manager.current_SetPath.pathNodes[manager.currentPath_NodeID].position - manager.transform.position);
        manager.transform.rotation = Quaternion.Slerp(manager.transform.rotation, object_Rotation, Time.deltaTime * manager.rotationSpeed);

        manager.gameObject.transform.rotation = manager.transform.rotation;

        if (node_Distance <= manager.waypointDist)
        {
            manager.currentPath_NodeID++;
        }

        if (manager.currentPath_NodeID >= manager.current_SetPath.pathNodes.Count)
        {
            manager.currentPath_NodeID = 0;
        }

        if (manager.FOV.PlayerDetected)
        {
            manager.SwitchState(manager.Detect);
        }
        
        if (Vector3.Distance(manager.gameObject.transform.position, manager.Player.transform.position) < manager.AttackRange)
        {
            manager.SwitchState(manager.Attack);
        }
    }
}
