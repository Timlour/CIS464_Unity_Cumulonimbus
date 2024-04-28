using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class RangeWeapon : MonoBehaviour {
    public Transform bulletSpawnPoint;
    public GameObject bulletPrefab;
    public float bulletSpeed = 10f;
    public bool isAI;
    public bool isAIFiring = false;
    public AudioSource weaponAudSrc;
    public AudioClip[] shootingSFX;
    private AudioClip Fire;
    public float delayedCD = 3f;
    private float depletionRate = .01f;
    private float currentCD;
    public bool spawnSoundFromGun = true;

    void Start(){
        weaponAudSrc = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        currentCD = currentCD - depletionRate;
        if(isAI){
            if(isAIFiring == true && currentCD <= 0.0f){
                var bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
                bullet.GetComponent<Rigidbody2D>().velocity = bulletSpawnPoint.up * bulletSpeed;
                if(spawnSoundFromGun == true){
                    Fire = shootingSFX[UnityEngine.Random.Range(0, shootingSFX.Length)];
                    weaponAudSrc.PlayOneShot(Fire);
                }
                currentCD = delayedCD;
            }
        }
    }

    public void WeaponFire(bool Trigger){
        isAIFiring = Trigger;
    }
}
