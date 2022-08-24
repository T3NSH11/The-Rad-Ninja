using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shuriken : MonoBehaviour
{
    GameObject EnemyObj;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            EnemyObj = other.gameObject;
            Destroy(gameObject);
            other.gameObject.GetComponent<Animator>().SetTrigger("Shot");
            StartCoroutine(Die());
        }
        else if (!other.gameObject.CompareTag("Enemy") && !other.gameObject.CompareTag("Cripto"))
        {
            Destroy(gameObject);
        }
    }

    IEnumerator Die()
    {
        yield return new WaitForSeconds(1);
        Destroy(EnemyObj);
    }
}
