using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int maxHealth = 100;
    int currentHealth;
    private AudioSource entityAudio;
    public bool isAlive = true; //BOOLEAN FOR IF ENTITY IS ACTIVE (TRUE BY DEFAULT)
    
    void Start()
    {
        entityAudio = GetComponent<AudioSource>();
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0 && isAlive)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Enemy died!");
        isAlive = false;
        Destroy(gameObject); // refers to gameObject script is attached to
    }

}
