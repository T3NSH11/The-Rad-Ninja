using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    public GameObject pauseMenu;

    public static bool isPaused;
    void Start()
    {
        pauseMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPaused)
            {
                PauseGame();
                Cursor.lockState = CursorLockMode.Confined;

            }
            else
            {
                ResumeGame();
                Cursor.lockState = CursorLockMode.Locked;



            }
        }   
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true); 
        Time.timeScale = 0f;
        isPaused = true;

        DialogueHandler.playerHUD.SetActive(false);
        if (DialogueHandler.dialogueActive)
            DialogueHandler.textbox.gameObject.SetActive(false);
        

    }
    
    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;

        if (DialogueHandler.dialogueActive)
            DialogueHandler.playerHUD.SetActive(true);
        else
            DialogueHandler.playerHUD.SetActive(true);
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }

    public void QuitGame()
    {
        Application.Quit(); 
    }

}
