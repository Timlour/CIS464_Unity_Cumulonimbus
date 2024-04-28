using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Kalhorn : MonoBehaviour
{
    [SerializeField] AudioClip[] enemySounds;
    private AudioClip clip;
    private Animator anim;
    private AudioSource audioSrc;
    public HealthBarOperator healthbar;

    enum EntitySelect {Player, Companion, Wanderer, Enemy};
    [SerializeField] EntitySelect entType;

    enum WeaponType {Melee, Range, Explosive, Special};
    [SerializeField] WeaponType[] weaponLoad = new WeaponType[2];

    public float RandomNum = 0f;

    private float initialHealth = 100f;
    public float maxHealth = 100f;
    public EntityHealth healthSrc;

    public bool isAlive = true;
    public bool isRevivable = false;
    public bool beingRevived = false;
    public float initialDownedHealth = 100f;
    private float healthLostPerSec = 1f;
    private bool DeathConfirmed = false;

    float startingX;
    int direction = 1;
    public float speed = 0.8f;
    public float walkSpeed = 0.8f;
    public float runSpeed = 1.2f;
    public float range = 3;
    public bool canMove = false; // If either player or enemy is moving and motion slack has finished.
    public bool Alerted = false;

    public int[] Hands = {0,0}; //0 = Empty Handed, 1 = Melee, 2 = Range Weapon, 3 = Special Item
    public bool isTwoHanded = false;
    public GameObject[] weapon;
    public string enemyType;

    // Start is called before the first frame update
    void Start()
    {
        if(enemyType == null){ enemyType = "unnamed"; }

        initialHealth = maxHealth;
        healthSrc = GetComponent<EntityHealth>();
        healthbar.SetHealth(healthSrc.LoadHealth(initialHealth), maxHealth);

        audioSrc = GetComponent<AudioSource>();

        anim = GetComponent<Animator>();

        startingX = transform.position.x;

        StartCoroutine(RandomSoundLoop());
        StartCoroutine(WeaponBehaviour());
        StartCoroutine(AIMovementTypeNav());
    }

    public void TakeDamage(float damage) {
        healthbar.SetHealth(healthSrc.ModifyHealth(damage, true), maxHealth);
        if (healthSrc.ModifyHealth(damage, true) <= 0f) {
            isAlive = false; 
        } 
        else{
            healthSrc.ModifyHealth(damage, true);
            PlaySFX(Random.Range(7,10));
        }
    }

    IEnumerator WeaponBehaviour(){
        yield return new WaitForSeconds(Random.Range(0,5));
        for(int i = 0; i<weapon.Length; i++){
            if(weapon[i].gameObject.GetComponent<RangeWeapon>()){
                if(entType == EntitySelect.Enemy){
                    int willFire = Random.Range(0,2);
                    if (willFire == 1){ 
                        weapon[i].gameObject.GetComponent<RangeWeapon>().WeaponFire(true);
                        anim.SetBool("isFiring", true);
                    }
                    else{ 
                        weapon[i].gameObject.GetComponent<RangeWeapon>().WeaponFire(false);
                        anim.SetBool("isFiring", false);
                    }
                }
                else if(entType == EntitySelect.Player){
                        weapon[i].gameObject.GetComponent<RangeWeapon>().WeaponFire(Input.GetKeyDown(KeyCode.F));
                }
            }
        }
        StartCoroutine(WeaponBehaviour());
    }

    IEnumerator RandomSoundLoop(){
        RandomNum = Random.Range(1,20);
        yield return new WaitForSeconds(Random.Range(0,5));
        if(RandomNum == Random.Range(10,20) && isAlive){ 
            clip = enemySounds[UnityEngine.Random.Range(0, 2)];
            audioSrc.PlayOneShot(clip);
        }
        if(isAlive && !DeathConfirmed){ 
            StartCoroutine(RandomSoundLoop()); 
        }
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
        if(canMove == true && isAlive) {
            anim.SetBool("isMoving", true);
            transform.Translate(Vector2.right * speed * Time.deltaTime * direction); //move right, Time.deltaTime to make framerate independent
            if (transform.position.x < startingX || transform.position.x > startingX + range)
            {
                direction *= -1; // change direction
            }
        }
        else{
            anim.SetBool("isMoving", false);
        }
        if(!isAlive && !DeathConfirmed){ 
            StartCoroutine(DownedDepletion());
        }
    }

    

    IEnumerator Die() {
        anim.SetBool("isDead", true); //Tells animator state of enemy they are dead.
        DeathConfirmed = true; //To make fixed update only repeat it once due to testing the toggle of isDead setting it to off.
        PlaySFX(4);
        if(UnityEngine.Random.Range(10,15) == 11){ //If the random number in RareInstance is equal to 11.
            yield return new WaitForSeconds(2); //Delays for 2 seconds before it can play the sound. Like every weak wrestler in the WWE going head to toe against The Undertaker.
            PlaySFX(5);
        }
        yield return new WaitForSeconds(5);
        Destroy(gameObject); // refers to gameObject script is attached to
    }

    IEnumerator DownedDepletion(){
        if(!isRevivable){ StartCoroutine(Die()); }
        else {
            yield return new WaitForSeconds(1);
            if(!beingRevived){
                TakeDamage(healthLostPerSec);
                PlaySFX(Random.Range(7,10));
            }
            StartCoroutine(DownedDepletion());
        }
    }

    void PlaySFX(int element){ clip = enemySounds[element]; audioSrc.PlayOneShot(clip); }
}
