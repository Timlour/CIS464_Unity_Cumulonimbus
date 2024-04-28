using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Tilemaps;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float horizontal;
    public float speed = 8f;
    public float jumpingPower = 16f;
    private bool isFacingRight = true;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    public Animator animMachine;
    public AudioSource audioSrc;
    public AudioClip[] leapSFX;
    private bool hasLeaped = false;
    [SerializeField] public List<string> _list;
    // Start is called before the first frame update
    
    void Start()
    {
        audioSrc = GetComponent<AudioSource>();
        animMachine = GetComponent<Animator>();
        StartCoroutine(PreFallingDelay());
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        animMachine.SetInteger("Movement", (int) horizontal);
        if (Input.GetButtonDown("Jump") && IsGrounded()) // Press Space Bar to jump
        {
            //Debug.Log("Ground is " + IsGrounded());
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
            //rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }
        else if(!IsGrounded()){
                if(!hasLeaped){
                    animMachine.SetTrigger("hasJumped");
                    audioSrc.PlayOneShot(leapSFX[0]);
                    hasLeaped = true;
                }
        }
        else if(IsGrounded()){
            if(hasLeaped == true){
                Debug.Log("Landed");
                animMachine.SetTrigger("hasLanded");
                audioSrc.PlayOneShot(leapSFX[1]);
                animMachine.SetBool("isFalling", true);
            }
            hasLeaped = false;
        }

        /*if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            
        }*/

        Flip();

    }

    IEnumerator PreFallingDelay(){
        yield return new WaitForSeconds(.5f);
        animMachine.SetBool("isFalling", true);
        StartCoroutine(PreFallingDelay());
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);

    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void Flip()
    {
        if ( (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f))
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    public void MovementStep(AnimationEvent e){
        audioSrc.PlayOneShot(leapSFX[Random.Range(2,3)]);
    }
}

