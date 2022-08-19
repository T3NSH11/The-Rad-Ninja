using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFOV : MonoBehaviour
{
    public float RangeRadius;
    [Range(0, 360)]

    public float FOVAngle;

    public LayerMask PlayerMask;
    public LayerMask WallMask;

    public bool PlayerDetected;

    public Vector3 directionToPlayer;
    public Transform PlayerTransform;
    public AudioSource source;
    public AudioClip clip;

    private void Update()
    {
        FieldOfViewCheck();
    }

    private void FieldOfViewCheck()
    {
        Collider[] CollidersInRange = Physics.OverlapSphere(transform.position, RangeRadius, PlayerMask);

        if (CollidersInRange.Length != 0)
        {
            PlayerTransform = CollidersInRange[0].transform;
            directionToPlayer = (PlayerTransform.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionToPlayer) < FOVAngle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, PlayerTransform.position);

                if (!Physics.Raycast(transform.position, directionToPlayer, distanceToTarget, WallMask))
                {
                    PlayerDetected = true;
                    source.PlayOneShot(clip);
                    Debug.Log("Detected");
                }
                else
                    PlayerDetected = false;
            }
            else
                PlayerDetected = false;
        }
        else if (PlayerDetected)
            PlayerDetected = false;
    }
}