using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    public GameObject pauseMenu;
    public GameObject HUD;

    public bool isPaused;
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
                HUD.SetActive(false);

            }
            else
            {
                ResumeGame();
                Cursor.lockState = CursorLockMode.Locked;
                HUD.SetActive(true);



            }
        }   
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true); 
        Time.timeScale = 0f;
        isPaused = true;
    }
    
    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
        Cursor.lockState = CursorLockMode.Locked;
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
