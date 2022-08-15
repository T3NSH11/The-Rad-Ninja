using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    #region Variables
    #region Enemy general
    EnemyState CurrentState;
    [Header("General Settings")]
    public GameObject EnemyHead;
    public GameObject Player;
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
    public EnemyState Detect;
    public EnemyState FollowPath;

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
    public float waypointDist;
    public float rotationSpeed;
    public GameObject[] patrolPaths;
    public GameObject[] PathNodes;
    public int currentPath_NodeID = 0;
    public WaypointSystem current_SetPath;
    #endregion

    #region Attack
    [Header("Attack Settings")]
    public float AttackRange;
    public float AttackDamage;
    public float Attackcooldown;
    #endregion

    #region Animations
    [Header("Animation Settings")]
    public float AnimationBlendSpeed = 2f;
    public float mDesiredAnimationSpeed = 0f;
    CharacterController MyController;
    public Animator MyAnimator;
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
        Detect = new Detect();
        FollowPath = new FollowPath();
        CurrentState = Wander;
        #endregion

        MyAnimator = GetComponent<Animator>();

        CurrentState.StartState(this);
    }

    void Update()
    {
        CurrentState.UpdateState(this);
        Debug.Log(CurrentState);
        MyAnimator.SetFloat("Speed", Mathf.Lerp(MyAnimator.GetFloat("Speed"), mDesiredAnimationSpeed, AnimationBlendSpeed * Time.deltaTime));
    }

    public void SwitchState(EnemyState state)
    {
        CurrentState = state;
        CurrentState.StartState(this);
    }
}
