using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PromptDisplayer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textbox;

    [SerializeField] string promptText;
    [SerializeField] KeyCode buttonToPress;

    bool promptActive = false;
    bool taskComplete = false;


    void Start()
    {
        textbox.gameObject.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !taskComplete)
        {
            textbox.gameObject.SetActive(true);
            textbox.text = promptText;
            promptActive = true;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(buttonToPress) && promptActive)
        {
            textbox.text = "";
            textbox.gameObject.SetActive(false);
            promptActive = false;
            taskComplete = true;
        }
    }
}
