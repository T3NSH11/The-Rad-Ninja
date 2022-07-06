using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    #region Variables
    #region Enemy general
    EnemyState CurrentState;
    GameObject EnemyHead;
    public Pathfinder Pathfinder = new Pathfinder();
    public EnemyFOV FOV;
    public float DetectionLevel;
    public float WanderDetectionRequired;
    public float AlertDetectionSpeedRequired;
    #region States
    public EnemyState Wander;
    public EnemyState SearchForPlayer;
    public EnemyState Alert;
    public EnemyState ChasePlayer;
    public EnemyState Idle;
    public EnemyState Attack;

    #endregion
    #endregion

    #region Search for player
    [Header("Search Settings")]
    public Vector3 LastPlayerLoc;
    public float RunSpeed;
    #endregion

    #region Wander
    [Header("Wander Settings")]
    [SerializeField]
    public GameObject[] WanderPath;
    public Stack<Vector3> _WanderPath;
    public float WalkSpeed;
    #endregion
    #endregion

    void Start()
    {
        #region State assignments
        Wander = new Wander();
        SearchForPlayer = new SearchForPlayer();
        Alert = new Alert();
        ChasePlayer = new ChasePlayer();
        Idle = new Idle();
        Attack = new Attack();
        CurrentState = new Wander();
        #endregion

        CurrentState.StartState(this);
    }

    void Update()
    {
        CurrentState.UpdateState(this);
    }

    public void SwitchState(EnemyState state)
    {
        CurrentState = state;
        CurrentState.StartState(this);
    }
}
