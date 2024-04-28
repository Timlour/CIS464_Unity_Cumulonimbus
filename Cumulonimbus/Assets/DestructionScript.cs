using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructionScript : MonoBehaviour
{
    public float life = 3; //The amount of seconds the bullet has left to last through. Like me after seeing my final exam score.

    public AudioSource audiSrc; //The destruction noise origin and source.

    public AudioClip[] destructSFX; //The variations of sounds for a destruction of such immense power.

    public AudioClip currentdestructSFX; //The currently assigned sound effect for blowing up like ASMR anime girl hitting you with a frying pan.

    void Awake(){ 

        audiSrc = GetComponent<AudioSource>(); // Get the audio source from the GameObject's bottom.

        currentdestructSFX = destructSFX[UnityEngine.Random.Range(0,destructSFX.Length)]; // Assign the random existing screech noise of your uncontrollable child.

        audiSrc.PlayOneShot(currentdestructSFX); // Make 'em see Jesus and scream in joy.

        Destroy(gameObject, life); //The bullet itself, seconds of lifetime. You then realize it went over the top. Hot damn...

    }
}
