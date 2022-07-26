using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shuriken : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Destroy(other.gameObject);
        }
        else if (!other.gameObject.CompareTag("Enemy") && !other.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
