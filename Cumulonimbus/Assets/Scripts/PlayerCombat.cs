using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PlayerCombat : MonoBehaviour
{
    public Transform attackPoint;
    public LayerMask enemyLayers;

    private Animator anim;
    private bool attackWithoutAnim = false;

    public GameObject[] weapon;

    public AudioSource weaponAudio;

    public AudioClip[] weaponNoise;

    private AudioClip currentWeaponNoise;

    public float attackRange = 0.5f;
    public float attackDamage = 10f;

    void Start(){
        anim = GetComponent<Animator>();
        weaponAudio = GetComponent<AudioSource>();
        StartCoroutine(RandomAttackMode());
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Attack1")) { 
            if(attackWithoutAnim){
                Attack(); 
            }
            else{
                anim.SetTrigger("Trigger Ability 1");
                anim.SetBool("Hold Ability 1", true);
            }
        }
        else if(Input.GetButtonUp("Attack1")){
            anim.SetBool("Hold Ability 1", false);
        }
    }

    public void WeaponNoise(AnimationEvent e){
        //if(type >= 0){
            currentWeaponNoise = weaponNoise[3]; weaponAudio.PlayOneShot(currentWeaponNoise); 
        //}
    }

    public void AnimationAttack(AnimationEvent e){ //Occurs at a frame in the animation clip it ticks.
        Attack();
    }

    public void Attack()
    {
        // Detect enemies in range of attack
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers); // creates circle from AttackPoint and collects all objects that circle hits
        
        // Damage them 
        foreach(Collider2D enemy in hitEnemies)
        {
            Debug.Log("We hit " + enemy.name);
            enemy.GetComponent<Kalhorn>().TakeDamage(attackDamage);
        }
    }

    private void OnDrawGizmosSelected() // Allows drawing in editor whenever object is selected
    {
        if(attackPoint == null) // failsafe
        {
            return;
        }
        
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    IEnumerator RandomAttackMode(){
        yield return new WaitForSeconds(1f);
        anim.SetInteger("AttackType", Random.Range(0,2));
        StartCoroutine(RandomAttackMode());
    }
}
