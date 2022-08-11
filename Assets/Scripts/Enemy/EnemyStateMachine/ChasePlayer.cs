using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasePlayer : EnemyState
{
    public Stack<Vector3> Path;
    public override void StartState(EnemyManager manager)
    {
        manager.mDesiredAnimationSpeed = 1f;
        manager.MyAnimator.SetBool("Moving", true);
        Path = manager.Pathfinder.FindPath(manager.gameObject.transform.position, manager.Player.transform.position);
    }

    public override void UpdateState(EnemyManager manager)
    {
        if(manager.FOV.PlayerDetected == true)
        {
            bool PathFollowed = manager.Pathfinder.FollowPath(Path, manager.gameObject, manager.RunSpeed);
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
