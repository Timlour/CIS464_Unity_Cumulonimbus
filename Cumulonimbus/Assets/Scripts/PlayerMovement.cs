using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
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
    private bool isCloseToLadder = false; // set true when player is near ladder but has not started climbing yet
    private bool climbHeld = false; // set true when player presses and holds climb button
    private bool hasStartedClimb = false; // control bool used to prevent other player actions while climbing
    private Transform ladder;
    private float vertical;
    private float climbSpeed = 0.2f;
    // Start is called before the first frame update
    void Start()
    {
        AudioManager.instance.Play("WorldOfPain");
        AudioManager.instance.Play("RainThunder");
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical") * climbSpeed;

        if (Input.GetKeyDown(KeyCode.A) && IsGrounded()) // Press A to jump
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
        }

        /*if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }*/

        Flip();

        climbHeld = (isCloseToLadder && Input.GetKey(KeyCode.DownArrow)) ? true : false;

    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);

        // Climbing
        if (hasStartedClimb && !climbHeld)
        {
            if (horizontal > 0 || horizontal < 0) ResetClimbing();
        }
        else if (hasStartedClimb && climbHeld)
        {
            float height = GetComponent<SpriteRenderer>().size.y;
            float topHandlerY = Half(ladder.transform.GetChild(0).transform.position.y + height);
            float bottomHandlerY = Half(ladder.transform.GetChild(1).transform.position.y + height);
            float transformY = Half(transform.position.y);
            float transformVY = transformY + vertical;

            if (transformVY > topHandlerY || transformVY < bottomHandlerY)
            {
                ResetClimbing();
            }
            else if (transformY <= topHandlerY && transformY >= bottomHandlerY)
            {
                rb.bodyType = RigidbodyType2D.Kinematic;
                if (!transform.position.x.Equals(ladder.transform.position.x))
                    transform.position = new Vector3(ladder.transform.position.x, transform.position.y, transform.position.z);

                Vector3 forwardDirection = new Vector3(0, transformVY, 0);
                Vector3 newPos = Vector3.zero;
                if (vertical > 0)
                    newPos = transform.position + forwardDirection * Time.deltaTime * climbSpeed;
                else if (vertical < 0)
                    newPos = transform.position - forwardDirection * Time.deltaTime * climbSpeed;
                if (newPos != Vector3.zero) rb.MovePosition(newPos);
            }
        }
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

    private void OnTriggerStay2D(Collider2D collision)
    {
 
        if (collision.gameObject.tag.Equals("Ladder"))
        {
            isCloseToLadder = true;
            this.ladder = collision.transform;
        }
    }

    public static float Half(float value)
    {
        return Mathf.Floor(value) + 0.5f;
    }

    private void ResetClimbing()
    {
        if (hasStartedClimb)
        {
            hasStartedClimb = false;
            rb.bodyType = RigidbodyType2D.Dynamic;
            transform.position = new Vector3(transform.position.x, Half(transform.position.y), transform.position.z);
        }
    }


}

