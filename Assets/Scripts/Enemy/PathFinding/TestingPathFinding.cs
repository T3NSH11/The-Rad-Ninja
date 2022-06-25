using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingPathFinding : MonoBehaviour
{
    public GameObject Origin;
    public GameObject Target;
    Pathfinder Pathfinder = new Pathfinder();
    public float Speed;
    public Stack<Vector3> Path;
    Vector3 TargetPos;

    void Start()
    {
        Path = Pathfinder.FindPath(Origin.transform.position, Target.transform.position);
    }

    private void Update()
    {
        if (TargetPos != Target.transform.position)
        {
            Path = Pathfinder.FindPath(Origin.transform.position, Target.transform.position);
        }

        TargetPos = Target.transform.position;
        Pathfinder.FollowPath(Path, Origin, Speed);
    }
}
