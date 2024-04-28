using UnityEngine.SceneManagement;
using UnityEngine;

public class MenuEvents : MonoBehaviour
{
    public void LoadLevel(int index) // function will take level index to load
    {
        SceneManager.LoadScene(index);
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Title Screen"); // Go to Title Screen
    }

    public void QuitGame()
    {
        Debug.Log("Quitting...");
        Application.Quit(); // Does not work in editor, only in build
    }
}
