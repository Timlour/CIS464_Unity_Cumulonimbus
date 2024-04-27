using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Kalhorn : MonoBehaviour
{
    [SerializeField] AudioClip[] enemySounds;
    private AudioClip clip;
    private Animator anim;
    public float RandomNum = 0f;
    public int maxHealth = 100; //ADDED IN FROM ENEMY
    int currentHealth; //ADDED IN
    AudioSource audioSrc; //ADDED IN
    public bool isAlive = true; //ADDED IN
    private bool DeathConfirmed = false;
    
    public float speed = 0.8f;
    public float range = 3;
    public bool canMove = true; // If either player or enemy is moving and motion slack has finished.

    float startingX;
    int direction = 1;
    // Start is called before the first frame update
    void Start()
    {
        audioSrc = GetComponent<AudioSource>(); //ADDED IN
        anim = GetComponent<Animator>();
        currentHealth = maxHealth; //ADDED IN
        startingX = transform.position.x;
        StartCoroutine(RandomSoundLoop());
    }

    public void TakeDamage(int damage) { //ADDED IN
        currentHealth -= damage; //ADDED IN
        if (currentHealth <= 0) { 
            isAlive = false;
         } //ADDED IN
    } //ADDED IN

    IEnumerator RandomSoundLoop(){
        RandomNum = Random.Range(1,20);
        yield return new WaitForSeconds(Random.Range(0,5));
        if(RandomNum == Random.Range(10,20) && isAlive){
            PlaySFX();
        }
        if(isAlive && !DeathConfirmed){
            StartCoroutine(RandomSoundLoop());
        }
    }

    void FixedUpdate()
    {
        if(canMove == true) {
            transform.Translate(Vector2.right * speed * Time.deltaTime * direction); //move right, Time.deltaTime to make framerate independent
            if (transform.position.x < startingX || transform.position.x > startingX + range)
            {
                direction *= -1; // change direction
            }
        }
        if(!isAlive && !DeathConfirmed){ 
            currentHealth = 0; 
            StartCoroutine(Die()); 
        }
    }

    IEnumerator Die() {
        anim.SetBool("isDead", true);
        DeathConfirmed = true;
        Debug.Log("Enemy died!");
        //isAlive = false;
        clip = enemySounds[4];
        audioSrc.PlayOneShot(clip);
        int RareInstance = UnityEngine.Random.Range(10,12);
        if(RareInstance == 11){
            yield return new WaitForSeconds(2);
            clip = enemySounds[5];
            audioSrc.PlayOneShot(clip);
        }
        yield return new WaitForSeconds(5);
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
    void PlaySFX(){
        clip = enemySounds[UnityEngine.Random.Range(0, 2)];
        audioSrc.PlayOneShot(clip);
    }
}
