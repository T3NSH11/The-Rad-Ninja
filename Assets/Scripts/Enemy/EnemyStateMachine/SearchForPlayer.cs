using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchForPlayer : EnemyState
{
    public Stack<Vector3> Path;

    public override void StartState(EnemyManager manager)
    {
        Path = manager.Pathfinder.FindPath(manager.gameObject.transform.position, manager.LastPlayerLoc);
        manager.mDesiredAnimationSpeed = 0.5f;
    }

    public override void UpdateState(EnemyManager manager)
    {
        bool PathFollowed = manager.Pathfinder.FollowPath(Path, manager.gameObject, manager.RunSpeed);

        if(PathFollowed == true)
        {
            manager.SwitchState(manager.FollowPath);
        }

        if(manager.FOV.PlayerDetected)
        {
            manager.SwitchState(manager.ChasePlayer);
        }
    }
}
