using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchForPlayer : EnemyState
{
    public Stack<Vector3> Path;
    public float SearchTimer = 5f;

    public override void StartState(EnemyManager manager)
    {
        Path = manager.Pathfinder.FindPath(manager.gameObject.transform.position, manager.LastPlayerLoc);
        manager.mDesiredAnimationSpeed = 0.5f;
        manager.MyAnimator.SetBool("Moving", true);
        SearchTimer = 5f;
    }

    public override void UpdateState(EnemyManager manager)
    {
        bool PathFollowed = manager.Pathfinder.FollowPath(Path, manager.gameObject, manager.RunSpeed);

        if(PathFollowed == true)
        {
            manager.MyAnimator.SetBool("Moving", false);
            SearchTimer -= Time.deltaTime;
        }

        if (SearchTimer <= 0)
        {
            manager.SwitchState(manager.FollowPath);
        }

        if(manager.FOV.PlayerDetected)
        {
            manager.SwitchState(manager.ChasePlayer);
        }

        if (Vector3.Distance(manager.transform.position, manager.Player.transform.position) < manager.AttackRange)
        {
            manager.SwitchState(manager.Attack);
        }
    }
}
