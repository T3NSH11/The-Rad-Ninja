using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPath : EnemyState
{
    Stack<Vector3> Path;
    public override void StartState(EnemyManager manager)
    {
        Path = manager.Pathfinder.FindPath(manager.transform.position, manager.current_SetPath.gameObject.transform.position);
    }

    public override void UpdateState(EnemyManager manager)
    {
        bool PathFollowed = manager.Pathfinder.FollowPath(Path, manager.gameObject, manager.WalkSpeed);

        if(PathFollowed)
        {
            manager.SwitchState(manager.Wander);
        }

        if(manager.FOV.PlayerDetected)
        {
            manager.SwitchState(manager.ChasePlayer);
        }
    }

}
