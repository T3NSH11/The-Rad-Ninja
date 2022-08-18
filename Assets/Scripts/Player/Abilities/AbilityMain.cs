using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityMain : MonoBehaviour
{
    //public bool enemyInRange;
    public bool takedownActive;
    public float TeleportRange;
    public float ShurikenSpeed;
    public float TeleportSpeed;
    public float takedownRange;
    public float distance;
    public GameObject TeleportMarker;
    public GameObject SmokeTrail;
    public GameObject Player;
    public GameObject Enemy;
    public GameObject ShurikenObj;
    public GameObject CameraObj;
    public GameObject Crosshair;
    public Renderer PlayerRenderer;
    public Animator PlayerAnim;
    public LayerMask GroundLayer;
    public LayerMask Climbable;
    public LayerMask ObstacleLayer;
    AbilityBase Ability;
        
    void Start()
    {
        Ability = new Takedown();
    }

    void Update()
    {
        Ability.Activation(this);
        //distance = Vector3.Distance(Player.transform.position, -Enemy.transform.forward.normalized + Enemy.transform.position);
    }

    public void SwitchState(AbilityBase NewAbility)
    {
        Ability = NewAbility;
    }


    // For testing purposes.
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Enemy = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Enemy = null;
        }
    }
}
