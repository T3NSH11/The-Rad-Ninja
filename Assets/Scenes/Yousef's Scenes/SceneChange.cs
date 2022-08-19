using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
   void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Cripto"))
        {
            SceneManager.LoadScene(2);
        }
    }
}
