using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerSFX : MonoBehaviour
{
    public AudioSource source;
    public AudioClip clip;
    private void OnTriggerEnter(Collider other)
    {
        source.PlayOneShot(clip);
    }
}
