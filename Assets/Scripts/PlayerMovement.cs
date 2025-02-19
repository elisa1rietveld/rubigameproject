using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;              // Movement speed
    private Rigidbody2D rb;
    public Animator animator;        // Animator for animations
    public SpriteRenderer spriteRenderer; // Sprite renderer to flip character

    // Movement direction
    private Vector2 moveDirection;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Get Rigidbody2D component
        spriteRenderer = GetComponent<SpriteRenderer>(); // Get SpriteRenderer
    }

    void Update()
    {
        // Input for movement
        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        float moveVertical = Input.GetAxisRaw("Vertical");

        // Check if the player is providing input and normalize the movement direction
        if (moveHorizontal != 0 || moveVertical != 0)
        {
            moveDirection = new Vector2(moveHorizontal, moveVertical).normalized;
        }
        else
        {
            moveDirection = Vector2.zero; // No movement if no input
        }

        // Handle animation based on movement
        float movementSpeed = moveDirection.magnitude;
        animator.SetFloat("Speed", movementSpeed);

        // Flip character based on movement direction
        if (moveHorizontal > 0)
        {
            spriteRenderer.flipX = false;
        }
        else if (moveHorizontal < 0)
        {
            spriteRenderer.flipX = true;
        }

        // Handle jumping animation
        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetBool("IsJumping", true);
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            animator.SetBool("IsJumping", false);
        }

        // Apply movement within screen bounds
        Vector3 pos = transform.position;
        Vector3 screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));

        pos.x = Mathf.Clamp(pos.x, -screenBounds.x, screenBounds.x);
        pos.y = Mathf.Clamp(pos.y, -screenBounds.y, screenBounds.y);

        transform.position = pos;
    }

    void FixedUpdate()
    {
        // Apply velocity to the Rigidbody2D based on movement direction and speed
        rb.velocity = moveDirection * speed;
    }
}
