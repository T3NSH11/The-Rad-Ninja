using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityMain : MonoBehaviour
{
    public float TeleportRange;
    public GameObject TeleportMarker;
    public GameObject Player;
    public LayerMask GroundLayer;
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
