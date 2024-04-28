using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Teleporter : MonoBehaviour
{
    public int sceneBuildIndex;
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Contact with teleporter");
        if (other.CompareTag("Player"))
        {
            Debug.Log("Contact with teleporter");
            // go to next level
            SceneManager.LoadScene(sceneBuildIndex, LoadSceneMode.Single);
        }
        
        
    }
}
