using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float horizontal;
    private float vertical;
    public float WalkingSpeed = 6f;
    public float RunningSpeed = 12f;
    public float speed = 8f;
    public float jumpingPower = 16f;
    private bool isFacingRight = true;

    private bool isSprinting;
    private Animator animator;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    // Start is called before the first frame update
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        horizontal = Input.GetAxisRaw("Horizontal"); //Default Input is A and D
        
        if (Input.GetButtonDown("Sprint")) //Default Input is Left Ctrl
        {
                speed = RunningSpeed;
                animator.SetFloat("WalkingSpeed", 2.0f);
        }
        else   
        {
                speed = WalkingSpeed;
                animator.SetFloat("WalkingSpeed", 1.0f);
        }

        if (Input.GetButtonDown("Jump") && IsGrounded()) //Default input is Space
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
        }

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f) //Default input is Space
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }

        Move();

    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }

    private bool IsGrounded()
    {
        animator.SetTrigger("Jump");
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void Move()
    {
        if ( (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f))
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
        //Animation state change
        if(horizontal != 0){
            animator.SetBool("isWalking", true);
        }
        else { 
            animator.SetBool("isWalking", false);
        }
    }

    
}

