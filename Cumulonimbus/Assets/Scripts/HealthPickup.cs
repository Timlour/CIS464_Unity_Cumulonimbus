using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HealthPickup : MonoBehaviour
{
    public int healAmount = 60;
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Contact with Item");
        if (other.CompareTag("Player"))
        {
            Debug.Log("Healing...");
            other.GetComponent<Health>().Heal(healAmount);
            Destroy(gameObject);
        }


    }
}
