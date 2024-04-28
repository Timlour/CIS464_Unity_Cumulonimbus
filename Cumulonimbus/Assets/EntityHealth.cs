using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityHealth : MonoBehaviour
{
    private float currentHealth;

    public float LoadHealth(float healthPoints){
        currentHealth = healthPoints;
        Debug.Log("Current Health on LoadHealth is " + currentHealth);
        return currentHealth;
    }

    public float ModifyHealth(float points, bool isDepleting){
        if(isDepleting){
            currentHealth -= points;
            Debug.Log("Current Health on ModifyHealth is " + currentHealth);
        }
        else{
            Debug.Log("Current Health on ModifyHealth is " + currentHealth);
            currentHealth += points;
        }
        return currentHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
