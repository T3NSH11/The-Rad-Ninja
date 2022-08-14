using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueInput : MonoBehaviour
{
    [SerializeField] string NPCName;
    [SerializeField] string[] dialogue;
    [SerializeField] AudioClip[] dialogueAudio;
    public GameObject Message;

    bool inRange;


    void Update(){

        if (inRange && !DialogueHandler.dialogueActive && Input.GetKeyDown(KeyCode.E))
        {
            DialogueHandler.StartDialogueDisplay(dialogue, dialogueAudio);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Cripto"))
            inRange = true;

        if (other.gameObject.tag == "Cripto")
        {
            Message.SetActive(true);
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Cripto"))
            inRange = false;

        if (other.gameObject.tag == "Cripto")
        {
            Message.SetActive(false);
        }
    }
}
