using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueHandler : MonoBehaviour
{
    
    static Image textbox;
    static TextMeshProUGUI displayName;
    static TextMeshProUGUI displayText; // make sure these are parented to the obj with the script

    static ThirdPersonController playerController;
    static GameObject playerHUD;

    static List<string> currentDialogueText = new List<string>(20);
    static List<AudioClip> currentDialogueAudio = new List<AudioClip>(20);
    static AudioSource audioSource;

    static int index = -1;
    public static bool dialogueActive {  get; private set; }


    static DialogueHandler handler;


    void Start(){

        if (handler == null)
            handler = this;
        else
            Destroy(this);


        audioSource = GetComponent<AudioSource>();

        textbox = gameObject.GetComponentInChildren<Image>();
        displayText = gameObject.GetComponentInChildren<TextMeshProUGUI>();


        playerController = GameObject.Find("Player").GetComponent<ThirdPersonController>();
        playerHUD = GameObject.Find("HUD");


        displayText.text = "";
        textbox.gameObject.SetActive(false);
        dialogueActive = false;

    }


    void Update(){
        
        if (dialogueActive && Input.GetKeyDown(KeyCode.Space))
        {
            DisplayNextLine();
        }
    }


    public static void StartDialogueDisplay(string[] dialogue, AudioClip[] audio){

        dialogueActive = true;
        // freeze the game, disable player movement
        playerController.enabled = false;
        playerHUD.SetActive(false);

        // make textbox appear
        textbox.gameObject.SetActive(true);


        currentDialogueText.Clear();
        currentDialogueText.AddRange(dialogue); // empty list from previous dialogue, add dialogue to be displayed

        currentDialogueAudio.Clear();
        currentDialogueAudio.AddRange(audio); // add respective audio


        index = -1;

    }

    static void DisplayNextLine(){


        index++;

        if (index >= currentDialogueText.Count) // if list is empty, resume game
        {
            dialogueActive = false;
            playerController.enabled = true;
            playerHUD.SetActive(true);

            displayText.text = "";
            textbox.gameObject.SetActive(false);

            return;
        }


        displayText.text = currentDialogueText[index]; // display text in the list at current index.

        //audioSource.clip = currentDialogueAudio[index];
        //audioSource.Play();

    }

}
