using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle : EnemyState
{
    public override void StartState(EnemyManager manager)
    {
        manager.MyAnimator.SetBool("Moving", false);
    }

    public override void UpdateState(EnemyManager manager)
    {

    }
}
