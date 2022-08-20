using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityMain : MonoBehaviour
{
    public bool takedownActive;
    public float TeleportRange;
    public float ShurikenSpeed;
    public float TeleportSpeed;
    public float BlinderSpeed;
    public float BlindRange;
    public float takedownRange;
    public float distance;
    public GameObject TeleportMarker;
    public GameObject SmokeTrail;
    public GameObject Player;
    public GameObject ShurikenObj;
    public GameObject CameraObj;
    public GameObject Crosshair;
    public GameObject BlindingOBJ;
    public GameObject Enemy;
    public Renderer PlayerRenderer;
    public Animator PlayerAnim;
    public LayerMask GroundLayer;
    public LayerMask Climbable;
    public LayerMask ObstacleLayer;
    AbilityBase Ability;
    public AudioSource source;
    public AudioClip clip;
        
    void Start()
    {
        Ability = new Teleport();
    }

    void Update()
    {
        Ability.Activation(this);
    }

    public void SwitchState(AbilityBase NewAbility)
    {
        Ability = NewAbility;
    }

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
