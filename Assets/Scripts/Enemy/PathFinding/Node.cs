using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Node
{
    public Vector3 WorldPosition;
    public Vector2 GridPosition;
    public float Gcost = Mathf.Infinity;
    public float Fcost = Mathf.Infinity;
    public float Hcost;
    public Node PreviousNode;
    public bool IsObstacle;
}
