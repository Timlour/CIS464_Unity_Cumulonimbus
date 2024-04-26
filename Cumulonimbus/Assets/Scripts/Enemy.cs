using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int maxHealth = 100;
    int currentHealth;
    private AudioSource entityAudio;
    public bool isAlive = true; //BOOLEAN FOR IF ENTITY IS ACTIVE (TRUE BY DEFAULT)
    

    // Start is called before the first frame update
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

    //void Update(){
        //if(!isAlive){
            //Die();
        //}
    //}

    void Die()
    {
        Debug.Log("Enemy died!");
        isAlive = false;
        
        //ADD FUNCTION TO PLAY DEATH SOUND
        //DELAY(DEATH ANIMATION)
        //RANDOM(SMALL RANGE FOR INSTANCE THAT A DEATH ANIMATION PLAYS)
        //IF/ELSE CONDITION FOR RARE DEATH SOUND AFTER DEATH ANIMATION FINISHES UP.
        //IF RARE DEATH IS TRUE
            //PLAY RARE DEATH SOUND
            //DELAY(RARE DEATH SOUND LENGTH + EXTRA DISAPPEAR TIME)
        //ELSE
            //DELAY(DISAPPEAR TIME)
        //Add a delay for the estimated death animation length and the enemy to disappear.
        //Disable the enemy
        Destroy(gameObject); // refers to gameObject script is attached to
    }

}
