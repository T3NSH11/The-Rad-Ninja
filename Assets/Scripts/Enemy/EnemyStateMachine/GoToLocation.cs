using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToLocation : EnemyState
{
    Pathfinder Pathfinder = new Pathfinder();
    public float Speed;
    public Stack<Vector3> Path;

    public override void StartState(EnemyManager manager)
    {
        throw new System.NotImplementedException();
    }

    public override void UpdateState(EnemyManager manager)
    {
        Path = Pathfinder.FindPath(manager.transform.position, manager.TargetLoc);

        Pathfinder.FollowPath(Path, manager.gameObject, Speed);
    }
}
