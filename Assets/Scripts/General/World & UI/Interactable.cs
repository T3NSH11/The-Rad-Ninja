using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//TODO: need to find a way to make sure the player can't interact with multiple things at once
public abstract class Interactable : MonoBehaviour
{
    public static bool interactionActive { get; protected set; }
    bool inRange;
    Image interactionIcon;


    

    void Start(){
        interactionIcon = GameObject.Find("Interaction Icon").GetComponent<Image>();
    }

    void Update(){

        if (inRange && !interactionActive && Input.GetKeyDown(KeyCode.K))
        {
            InteractionEvent();
        }
    }


    public abstract void OnInteraction();

    void InteractionEvent(){

        interactionActive = true;
        OnInteraction();
        interactionActive = false;
        
    }


    public void InRange(){
        inRange = true;
        interactionIcon.enabled = true;
        // use outline shader here.
        
    }

    public void OutOfRange(){
        inRange = false;
        interactionIcon.enabled = false;
    }


    /*
    private void OnTriggerEnter(Collider other){

        if (other.CompareTag("Player"))
        {
            // highlight gameobject
            // display interactable icon on canvas
            inRange = true;
            interactionIcon.enabled = true;
        }
    }

    private void OnTriggerExit(Collider other){

        if (other.CompareTag("Player"))
        {
            // dehighlight gameobject
            // remove interactable icon from canvas
            inRange = false;
            interactionIcon.enabled = false;
        }
    }*/
}
