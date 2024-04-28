using UnityEngine.SceneManagement;
using UnityEngine;

public class MenuEvents : MonoBehaviour
{
    public void LoadLevel(int index) // function will take level index to load
    {
        SceneManager.LoadScene(index);
    }
}
