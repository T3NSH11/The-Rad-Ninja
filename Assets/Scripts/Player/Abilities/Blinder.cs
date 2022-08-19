using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blinder : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<EnemyFOV>().enabled = false;
        }
        GameObject.Destroy(gameObject);
    }
}
