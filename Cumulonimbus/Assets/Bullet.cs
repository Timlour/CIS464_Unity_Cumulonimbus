using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour

{
    public int bulletDamage = 20; // Damage for bullets

    public string OwnerTag; //The tag of the person shooting the bullet. Imagine if bullets had embedded signatures. Killer Bean for example.

    public GameObject destructionPrefab; //When the bullet hits something, an audioObject is spawned at the location of bullet's death.
    
    public float life = 3; //The amount of seconds the bullet has left to last through.
    
    void Awake(){ 

        Destroy(gameObject, life); //The bullet itself, seconds of lifetime. It has no life and no value.

    }

    void OnCollisionEnter2D(){

        var destruct = Instantiate(destructionPrefab, 
        this.transform.position, this.transform.rotation); //This will spawn an explosion upon death.

        Destroy(gameObject); //Destroys itself to make way for game performance when handling things.

    }

    private void OnTriggerEnter2D(Collider2D other) // Had to set Laser Bullet Circle Collider 2D - Is Trigger to true. Seems to disable the destruction of the object, but is necessary for player damage.
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("We are hit");
            other.GetComponent<Health>().TakeDamage(bulletDamage);
        }
    }
}
