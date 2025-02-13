using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rb;
    public Animator animator;
    public SpriteRenderer spriteRenderer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        rb.velocity = new Vector2(moveHorizontal * speed, moveVertical * speed);

        float movementSpeed = new Vector2(moveHorizontal, moveVertical).magnitude;
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

        Vector3 pos = transform.position;
        Vector3 screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));

        pos.x = Mathf.Clamp(pos.x, -screenBounds.x, screenBounds.x);
        pos.y = Mathf.Clamp(pos.y, -screenBounds.y, screenBounds.y);

        transform.position = pos;
    }
}

