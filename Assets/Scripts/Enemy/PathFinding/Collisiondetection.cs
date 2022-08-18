using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collisiondetection : MonoBehaviour
{
    public Node node;
    public Pathfinder pathfinder;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
        {
            node.IsObstacle = true;
        }

        if (other.gameObject.CompareTag("Cripto"))
        {
            pathfinder.TargetNode = node;
        }

        if (other.gameObject.CompareTag("Enemy"))
        {
            pathfinder.OriginNode = node;
        }
    }
}
