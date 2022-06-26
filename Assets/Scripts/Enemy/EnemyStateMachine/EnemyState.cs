using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyState
{
    public abstract void UpdateState(EnemyManager manager);
    public abstract void StartState(EnemyManager manager);
}
