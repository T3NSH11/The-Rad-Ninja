using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueInput : MonoBehaviour
{
    [SerializeField] string NPCName;
    [SerializeField] string[] dialogue;
    [SerializeField] AudioClip[] dialogueAudio;

    bool inRange;


    void Update(){

        if (inRange && !DialogueHandler.dialogueActive && Input.GetKeyDown(KeyCode.Space))
        {
            DialogueHandler.StartDialogueDisplay(dialogue, dialogueAudio);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
            inRange = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            inRange = false;
    }
}
