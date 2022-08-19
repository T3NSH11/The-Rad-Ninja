using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerSFX : MonoBehaviour
{
    public AudioSource source;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Cripto"))
        {
            source.Play();
            if (this.CompareTag("Whisper"))
            {
                Destroy(this.gameObject);
            }
        }
    }
}
