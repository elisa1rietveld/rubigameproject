using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;               
    public float crouchSpeedMultiplier = 0.5f; 
    private Rigidbody2D rb;
    public Animator animator;        
    public SpriteRenderer spriteRenderer; 

    private Vector2 moveDirection;
    private bool isCrouching = false;  

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); 
        spriteRenderer = GetComponent<SpriteRenderer>(); 
    }

    void Update()
    {
        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        float moveVertical = Input.GetAxisRaw("Vertical");

        if (moveHorizontal != 0 || moveVertical != 0)
        {
            moveDirection = new Vector2(moveHorizontal, moveVertical).normalized;
        }
        else
        {
            moveDirection = Vector2.zero; 
        }

        float movementSpeed = moveDirection.magnitude;
        animator.SetFloat("Speed", movementSpeed);

        if (moveHorizontal > 0)
        {
            spriteRenderer.flipX = false;
        }
        else if (moveHorizontal < 0)
        {
            spriteRenderer.flipX = true;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetBool("IsJumping", true);
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            animator.SetBool("IsJumping", false);
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            animator.SetBool("IsCrouching", true);
            isCrouching = true;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            animator.SetBool("IsCrouching", false);
            isCrouching = false;
        }

        Vector3 pos = transform.position;
        Vector3 screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));

        pos.x = Mathf.Clamp(pos.x, -screenBounds.x, screenBounds.x);
        pos.y = Mathf.Clamp(pos.y, -screenBounds.y, screenBounds.y);

        transform.position = pos;
    }

    void FixedUpdate()
    {
        float currentSpeed = isCrouching ? speed * crouchSpeedMultiplier : speed;
        rb.velocity = moveDirection * currentSpeed;
    }
}
