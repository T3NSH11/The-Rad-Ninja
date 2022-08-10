using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using TMPro;

/// <summary>
///  Attach this to artifact and painting GameObjects, and fill the parameters with relevant info.
/// </summary>
public class ArtifactInteractable : Interactable
{
    GameObject artifactInfoDisplay;

    [SerializeField] string itemName;
    [SerializeField] string itemDescription;
    TextMeshProUGUI nameTextbox;
    TextMeshProUGUI descTextbox;

    Image itemImageUI;
    [SerializeField] Texture2D itemPortrait;


    [SerializeField] AudioClip narration;
    AudioSource audioSource;


    ParticleSystem particles;

    float previousTimescale = 0f;

    
    public override void OnInteraction()
    { 

        previousTimescale = Time.timeScale;
        Time.timeScale = 0f; // pause game


        DialogueHandler.playerHUD.SetActive(false);
        artifactInfoDisplay.SetActive(true);

        nameTextbox.text = itemName;
        descTextbox.text = itemDescription; // have text appear.

        //itemImageTexture = AssetPreview.GetMiniThumbnail(this.gameObject);
        itemImageUI.sprite = Sprite.Create(itemPortrait, 
                             new Rect(new Vector2(0, 0), new Vector2(itemPortrait.width, itemPortrait.height)), 
                             new Vector2(0, 0)); // set image to given object thumbnail


        //audioSource.Play(); // play narration

        // activate the relevant powerup here, if there is one.
        // eg: player.powerup.enabled = true; // set powerup in inspector


        //Debug.Log("interacted");
        StartCoroutine(WaitForContinue());
    }

    IEnumerator WaitForContinue(){

        yield return null;

        if (Input.GetKeyDown(interactionKey))
        {

            Time.timeScale = previousTimescale; // resume game

            nameTextbox.text = "";
            descTextbox.text = ""; 
            artifactInfoDisplay.SetActive(false);
            DialogueHandler.playerHUD.SetActive(true);
            
            //particles.Play();
            //particles.Emit(50);
            //transform.DetachChildren();
            //this.gameObject.SetActive(false);

            gameObject.GetComponent<MeshRenderer>().material.color = new Color(0.6f, 0.6f, 0.6f);

            interactionActive = false;
            interactionIcon.gameObject.SetActive(true);
            //hasBeenChecked = true;

            yield break; // exit waiiting loop
        }

        StartCoroutine(WaitForContinue());

    }

    private void Awake(){

        artifactInfoDisplay = GameObject.Find("Artifact Info Display");
        nameTextbox = artifactInfoDisplay.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        descTextbox = artifactInfoDisplay.transform.GetChild(2).GetComponent<TextMeshProUGUI>();
        itemImageUI = artifactInfoDisplay.transform.GetChild(3).GetComponent<Image>();
        //nameTextbox = artifactInfoDisplay.GetComponentInChildren<TextMeshProUGUI>();

        base.Awake();
    }

    private void Start()
    {
        if (artifactInfoDisplay.activeInHierarchy)
            artifactInfoDisplay.SetActive(false);

        audioSource = GetComponent<AudioSource>();
        //textbox = GameObject.Find("Item Description Box").GetComponent<TextMeshProUGUI>();

        //particles = transform.GetChild(0).GetComponent<ParticleSystem>();
        //particles.Stop();

        base.Start();
    }
}
