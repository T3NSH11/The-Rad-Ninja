using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityMain : MonoBehaviour
{
    public float TeleportRange;
<<<<<<< Updated upstream
    public float ShurikenSpeed;
=======
    public float TeleportSpeed;
>>>>>>> Stashed changes
    public GameObject TeleportMarker;
    public GameObject Player;
    public GameObject ShurikenObj;
    public GameObject CameraObj;
    public GameObject Crosshair;
    public LayerMask GroundLayer;
    public LayerMask Climbable;
    AbilityBase Ability;
        
    void Start()
    {
        Ability = new Teleport();
    }

    void Update()
    {
        Ability.Activation(this);
    }
}
