using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    EnemyState CurrentState;

    #region Go to location
    [Header("PathFinding Settings")]
    public Pathfinder Pathfinder = new Pathfinder();
    public Vector3 TargetLoc;
    public float RunSpeed;
    #endregion

    #region Wander
    [Header("Wander Settings")]
    [SerializeField]
    public GameObject[] WanderPath;
    public Stack<Vector3> _WanderPath;
    public float WalkSpeed;
    #endregion

    void Start()
    {
        for (int i = 0; i < WanderPath.Length; i++)
        {
            _WanderPath.Push(WanderPath[i].transform.position);
        }

        CurrentState.StartState(this);
    }

    void Update()
    {
        CurrentState.UpdateState(this);
    }

    public void SwitchState(EnemyState state)
    {
        CurrentState = state;
    }
}
