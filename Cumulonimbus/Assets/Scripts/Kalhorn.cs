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
    public int maxHealth = 100;
    int currentHealth;
    AudioSource audioSrc;
    public bool isAlive = true;
    private bool DeathConfirmed = false;
    public float speed = 0.8f;
    public float range = 3;
    public bool canMove = false; // If either player or enemy is moving and motion slack has finished.
    public int[] Hands = {0,0}; //0 = Empty Handed, 1 = Melee, 2 = Range Weapon, 3 = Special Item
    public bool isTwoHanded = false;
    public GameObject[] weapon;
    public string enemyType;
    public bool Alerted = false;

    float startingX;
    int direction = 1;
    // Start is called before the first frame update
    void Start()
    {
        if(enemyType == null){ enemyType = "unnamed"; }
        audioSrc = GetComponent<AudioSource>(); //ADDED IN
        anim = GetComponent<Animator>();
        currentHealth = maxHealth; //ADDED IN
        startingX = transform.position.x;
        StartCoroutine(RandomSoundLoop());
        StartCoroutine(WeaponBehaviour());
        StartCoroutine(AIMovementTypeNav());
    }

    public void TakeDamage(int damage) {
        currentHealth -= damage; 
        if (currentHealth <= 0) { isAlive = false; }
    }

    IEnumerator WeaponBehaviour(){
        yield return new WaitForSeconds(Random.Range(0,5));
        for(int i = 0; i<weapon.Length; i++){
            if(weapon[i].gameObject.GetComponent<RangeWeapon>()){
                int willFire = Random.Range(0,2);
                if (willFire == 1){ 
                    weapon[i].gameObject.GetComponent<RangeWeapon>().WeaponFire(true);
                }
                else{ 
                    weapon[i].gameObject.GetComponent<RangeWeapon>().WeaponFire(false);
                }
            }
        }
        StartCoroutine(WeaponBehaviour());
    }

    IEnumerator RandomSoundLoop(){
        RandomNum = Random.Range(1,20);
        yield return new WaitForSeconds(Random.Range(0,5));
        if(RandomNum == Random.Range(10,20) && isAlive){ PlaySFX(); }
        if(isAlive && !DeathConfirmed){ StartCoroutine(RandomSoundLoop()); }
    }

    IEnumerator AIMovementTypeNav(){
        int RNG_MovingNav = Random.Range(0,2);
        yield return new WaitForSeconds(Random.Range(0,5));
        if(RNG_MovingNav == 1){
            canMove = true;
        }
        else{
            canMove = false;
        }
        StartCoroutine(AIMovementTypeNav());
    }

    void FixedUpdate()
    {
        if(canMove == true) {
            anim.SetBool("isMoving", true);
            transform.Translate(Vector2.right * speed * Time.deltaTime * direction); //move right, Time.deltaTime to make framerate independent
            if (transform.position.x < startingX || transform.position.x > startingX + range)
            {
                direction *= -1; // change direction
                Debug.Log(direction);
            }
        }
        else{
            anim.SetBool("isMoving", false);
        }
        if(!isAlive && !DeathConfirmed){ 
            currentHealth = 0; 
            StartCoroutine(Die()); 
        }
    }

    IEnumerator Die() {
        anim.SetBool("isDead", true); //Tells animator state of enemy they are dead.
        DeathConfirmed = true; //To make fixed update only repeat it once due to testing the toggle of isDead setting it to off.
        clip = enemySounds[4]; //Gets the clip in Element 4 which is a death sound.
        audioSrc.PlayOneShot(clip); //Plays the Death Sound.
        int RareInstance = UnityEngine.Random.Range(10,15); //Initializes a random number to match with an instance to do a thing.
        if(RareInstance == 11){ //If the random number in RareInstance is equal to 11.
            yield return new WaitForSeconds(2); //Delays for 2 seconds before it can play the sound. Like every weak wrestler in the WWE going head to toe against The Undertaker.
            clip = enemySounds[5]; //Gets the clip in Element 5 which is a death sound.
            audioSrc.PlayOneShot(clip); //Plays the Rare Death Sound
        }
        yield return new WaitForSeconds(5);
        Destroy(gameObject); // refers to gameObject script is attached to
    }
    void PlaySFX(){
        clip = enemySounds[UnityEngine.Random.Range(0, 2)];
        audioSrc.PlayOneShot(clip);
    }
}
