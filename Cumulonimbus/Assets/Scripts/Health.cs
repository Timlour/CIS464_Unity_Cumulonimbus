using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHealth = 600;
    public int currentHealth;
    public GameObject gameOverMenu;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth; // Refreshes health when transitioning from Spaceship to Area1
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Debug.Log("You died!");
            Destroy(gameObject);
            Time.timeScale = 0f; // freeze time
            gameOverMenu.SetActive(true); // enable Game Over Menu
        }
    }
}
