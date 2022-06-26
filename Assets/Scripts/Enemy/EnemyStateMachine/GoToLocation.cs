using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToLocation : EnemyState
{
    public Stack<Vector3> Path;

    public override void StartState(EnemyManager manager)
    {
        throw new System.NotImplementedException();
    }

    public override void UpdateState(EnemyManager manager)
    {
        Path = manager.Pathfinder.FindPath(manager.transform.position, manager.TargetLoc);

        manager.Pathfinder.FollowPath(Path, manager.gameObject, manager.RunSpeed);
    }
}
