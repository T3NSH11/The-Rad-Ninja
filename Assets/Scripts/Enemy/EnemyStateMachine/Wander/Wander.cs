using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wander : EnemyState
{
    bool PathReversed;
    Stack<Vector3> Path;
    Stack<Vector3> ReversePath;
    public override void StartState(EnemyManager manager)
    {
        Path = manager._WanderPath;

        foreach (Vector3 position in Path)
        {
            ReversePath.Push(Path.Pop());
        }
    }

    public override void UpdateState(EnemyManager manager)
    {
        if(manager.Pathfinder.PathFollowed == true)
        {
            if (PathReversed == false)
            {
                Path = ReversePath;

                manager.SwitchState(new Idle());
            }

            if (PathReversed == true)
            {
                Path = manager._WanderPath;
            }
        }

        manager.Pathfinder.FollowPath(Path, manager.transform.gameObject, manager.WalkSpeed);
    }
}
