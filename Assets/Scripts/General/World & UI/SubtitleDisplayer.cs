using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SubtitleDisplayer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI subtitleTMP;
    [SerializeField] string[] textToDisplay;

    [SerializeField] AudioClip[] clips;
    AudioSource audioSource;

    [SerializeField] float timeTillNextLine;

    static bool subtitlesActive;
    bool hasBeenActivated;

    int index;

    private void Awake()
    {
        subtitleTMP = GameObject.Find("Subtitles").GetComponent<TextMeshProUGUI>();
        audioSource = GetComponent<AudioSource>(); 
    }
    void Start()
    {
        subtitleTMP.gameObject.SetActive(false);
        audioSource.loop = false;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !subtitlesActive && !hasBeenActivated)
        {
            subtitleTMP.gameObject.SetActive(true);
            subtitlesActive = true;

            index = -1;

            StartCoroutine(DisplaySubs());
        }
    }

    IEnumerator DisplaySubs()
    {

        index++;

        if (index >= textToDisplay.Length)
        {
            subtitleTMP.gameObject.SetActive(false);
            subtitlesActive = false;

            hasBeenActivated = true;

            yield break;
        }

        audioSource.clip = clips[index];
        audioSource.Play();

        subtitleTMP.text = textToDisplay[index];


        yield return new WaitForSeconds(clips[index].length + timeTillNextLine);
        StartCoroutine(DisplaySubs());
    }

}
