using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//TODO: need to find a way to make sure the player can't interact with multiple things at once
public abstract class Interactable : MonoBehaviour
{

    [SerializeField] protected KeyCode interactionKey = KeyCode.K;

    public static bool interactionActive { get; protected set; }
    protected bool hasBeenChecked;
    bool inRange;
    //GameObject playerHUD;

    protected Image interactionIcon;
    //Texture2D interactionIconTexture;




    protected void Awake()
    {
        interactionActive = false;

        interactionIcon = GameObject.Find("Interaction Icon").GetComponent<Image>();
    }

    protected void Start(){

        if(interactionIcon.gameObject.activeInHierarchy)
            interactionIcon.gameObject.SetActive(false);

        //playerHUD = GameObject.Find("HUD");

    }

    void Update(){

        if (inRange && !interactionActive && Input.GetKeyDown(interactionKey))
        {
            // play the "interacting" animation
            InteractionEvent();
        }

        if (interactionActive)
        {
            interactionIcon.gameObject.SetActive(false);
            //DialogueHandler.playerHUD.SetActive(false);
        }

    }


    public abstract void OnInteraction(); // what the object does when you interact with it goes here.

    void InteractionEvent(){

        interactionActive = true;
        OnInteraction();
        //interactionActive = false;
        
    }


    public void EnterRange(){
        inRange = true;
        interactionIcon.gameObject.SetActive(true);
        // use outline shader here.
    }

    public void ExitRange(){
        inRange = false;
        interactionIcon.gameObject.SetActive(false);
    }


    
    private void OnTriggerEnter(Collider other){

        if (other.CompareTag("Cripto") && !hasBeenChecked)
        {
            // highlight gameobject
            // display interactable icon on canvas
            this.EnterRange();
        }
    }

    private void OnTriggerExit(Collider other){

        if (other.CompareTag("Cripto"))
        {
            // dehighlight gameobject
            // remove interactable icon from canvas
            this.ExitRange();
        }
    }
}
