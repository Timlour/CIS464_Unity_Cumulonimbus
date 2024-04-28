using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxTurret : MonoBehaviour
{
    public float Range = 3f;

    public Transform Target;

    public bool Detected = false;
    Vector2 Direction;

    public AudioSource audioSrc;
    public AudioClip alert;
    public AudioClip conclude;

    public GameObject lGun;
    public GameObject rGun;
    public GameObject Bullet;

    public float FireRate;
    public float Force;

    public Transform ShootPoint1;
    public Transform ShootPoint2;

    public LayerMask enemyLayers;

    float nextTimeToFire = 0;

    void Start(){
        audioSrc = GetComponent<AudioSource>();
    }

    public Vector2 FindClosestEnemy(){
        float distanceToClosestEnemy = Mathf.Infinity;
        Kalhorn closestEnemy = null;
        Kalhorn[] allEnemies = GameObject.FindObjectsOfType<Kalhorn>();

        foreach (Kalhorn currentEnemy in allEnemies) {
            float distanceToEnemy = (currentEnemy.transform.position - this.transform.position).sqrMagnitude;
            if(distanceToEnemy < distanceToClosestEnemy) {
                distanceToClosestEnemy = distanceToEnemy;
                closestEnemy = currentEnemy;
            }
        }
        return closestEnemy.transform.position;
    }

    void Update(){

        Vector2 targetPos = FindClosestEnemy();
        Direction = targetPos - (Vector2)transform.position;
        RaycastHit2D rayInfo = Physics2D.Raycast(transform.position, Direction, Range);
        
        if(rayInfo)
        {
            //Debug.Log(rayInfo.collider.CompareTag("Enemy"));
            if(rayInfo.collider.CompareTag("Enemy"))
            {
                if(Detected == false)
                { 
                    //Debug.Log("Detected set to true");
                    audioSrc.PlayOneShot(alert); 
                    Detected = true; 
                }
            }
            else
            {
                if(Detected == true)
                { 
                    //Debug.Log("Detected set to false");
                    audioSrc.PlayOneShot(conclude);
                    Detected = false; 
                }
            }
        }
        if(Detected)
        {
            lGun.transform.up = Direction;
            rGun.transform.up = Direction;
            if(Time.time > nextTimeToFire){
                nextTimeToFire = Time.time+1/FireRate;
                shoot();
                //Debug.Log("DETECTED");
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.CompareTag("Player")){
            Debug.Log("HAS PLAYER");
        }
    }

    void shoot(){
        GameObject BulletIns1 = Instantiate(Bullet, ShootPoint1.position, Quaternion.identity);
        BulletIns1.GetComponent<Rigidbody2D>().AddForce(Direction * Force);
        GameObject BulletIns2 = Instantiate(Bullet, ShootPoint2.position, Quaternion.identity);
        BulletIns2.GetComponent<Rigidbody2D>().AddForce(Direction * Force);
    }

    void OnDrawGizmosSelected(){
        Gizmos.DrawWireSphere(transform.position, Range);
    }

}
