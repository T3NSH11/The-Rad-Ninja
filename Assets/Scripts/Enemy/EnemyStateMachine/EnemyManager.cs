using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    EnemyState CurrentState;

    [Header("PathFinding Settings")]
    #region Go to location
    public Vector3 TargetLoc;
    #endregion

    void Start()
    {
        CurrentState.StartState(this);
    }

    void Update()
    {
        CurrentState.UpdateState(this);
    }
}
