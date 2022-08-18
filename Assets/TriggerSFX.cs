using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerSFX : MonoBehaviour
{
    public AudioSource source;
    
    private void OnTriggerEnter(Collider other)
    {
        source.Play();
        Destroy(this.gameObject);
    }
}
