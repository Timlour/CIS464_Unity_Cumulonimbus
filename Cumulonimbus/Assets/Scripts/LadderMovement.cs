using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderMovement : MonoBehaviour
{
    private float vertical; // Move vertically
    private float speed = 8f;
    private bool isLadder; // check if player is next to ladder
    private bool isClimbing; // check if player is climbing or not

    [SerializeField] private Rigidbody2D rb;


    // Update is called once per frame
    void Update()
    {
        vertical = Input.GetAxis("Vertical"); // parse the return value of Input.GetAxis("Vertical") into our vertical variable - returns 1 or -1 depending on which button is pressed

        if (isLadder && Mathf.Abs(vertical) > 0f)
        {
            isClimbing = true;
        }
    }

    private void FixedUpdate() // Needed to work with physics
    {
        if (isClimbing)
        {
            Debug.Log("Climbing...");
            rb.gravityScale = 0f; // set rb's gravity scale to 0
            rb.velocity = new Vector2(rb.velocity.x, vertical * speed); // velocity is vertical value multiplied by speed, leave horizontal value as is
        }
        else
        {
            rb.gravityScale = 4f; // set gravity back to normal value
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) // to check if player is standing next to a ladder
    {
        if (collision.CompareTag("Ladder")) // if tag of collided object is Ladder
        {
            isLadder = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision) // to check if player is standing next to a ladder
    {
        if (collision.CompareTag("Ladder")) // if tag of collided object is Ladder
        {
            isLadder = false;
            isClimbing = false;
        }
    }
}
