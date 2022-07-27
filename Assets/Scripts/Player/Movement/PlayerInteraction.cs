using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    Interactable interactableToCheck;
    Interactable currentInteractable;

    Ray interactionCheckRay;
    RaycastHit hit;
    [SerializeField] float checkDistance = 3f;

    
    void Update(){

        interactionCheckRay = new Ray(transform.position, transform.forward * checkDistance);

        if (Physics.Raycast(interactionCheckRay, out hit, checkDistance) && !Interactable.interactionActive)
        {
            if (hit.collider.CompareTag("Interactable"))
            {
                interactableToCheck = hit.collider.gameObject.GetComponent<Interactable>(); // 

                if (currentInteractable == interactableToCheck)
                    currentInteractable.InRange();
                
                else // make sure our current loaded interactable is out of range before putting the new found interactable in range
                {
                    currentInteractable.OutOfRange(); 
                    currentInteractable = interactableToCheck;
                }

            }
        }

    }

    private void OnDrawGizmos(){
        Debug.DrawRay(transform.position, transform.forward * checkDistance);
    }
}
