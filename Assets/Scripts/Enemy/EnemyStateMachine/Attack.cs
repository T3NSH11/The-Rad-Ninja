using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : EnemyState
{
    float Attackcooldown;
    public override void StartState(EnemyManager manager)
    {
        manager.Player.GetComponent<PlayerHealth>().TakeDamage(manager.AttackDamage);
    }

    public override void UpdateState(EnemyManager manager)
    {
        manager.transform.LookAt(new Vector3(manager.Player.transform.position.x, manager.transform.position.y, manager.Player.transform.position.z));
        if (manager.Attackcooldown <= 0)
        {
            manager.MyAnimator.SetBool("Moving", false);
            manager.MyAnimator.SetTrigger("Attack");
            manager.Player.GetComponent<PlayerHealth>().TakeDamage(manager.AttackDamage);
            Attackcooldown = 2;
        }
        else
            manager.Attackcooldown -= Time.deltaTime;

        //if(!manager.FOV.PlayerDetected)
        //{
        //    manager.SwitchState(manager.SearchForPlayer);
        //}

        if(Vector3.Distance(manager.transform.position, manager.Player.transform.position) > manager.AttackRange)
        {
            manager.SwitchState(manager.ChasePlayer);
        }
    }
}
