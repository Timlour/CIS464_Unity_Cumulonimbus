using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public bool isPaused; 
    // Start is called before the first frame update
    void Start()
    {
        pauseMenu.SetActive(false); // Pause menu disabled on scene start
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(isPaused)
            {
                ResumeGame(); // resume if paused
            }
            else
            {
                PauseGame(); // pause if not paused
            }
        }
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true); // enable pause menu
        Time.timeScale = 0f; // freeze time
        isPaused = true; // paused
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false); // disable pause menu
        Time.timeScale = 1f; // unfreeze time
        isPaused = false; // not paused
    }

    /*public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Title Screen"); // Go to Title Screen
    }

    public void QuitGame()
    {
        Application.Quit(); // Does not work in editor, only in build
    }*/
}
