using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Marrow : MonoBehaviour
{
    public int contactDamage = 180;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("We are hit");
            other.GetComponent<Health>().TakeDamage(contactDamage);
        }
    }
}
