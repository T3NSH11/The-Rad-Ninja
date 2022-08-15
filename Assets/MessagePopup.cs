using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessagePopup : MonoBehaviour
{

    [SerializeField] public GameObject Message;

     private void OnTriggerEnter(Collider other)
    {
     if(other.gameObject.tag == "Cripto")
        {
            Message.SetActive(true);
        }   
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Cripto")
        {
            Message.SetActive(false);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
