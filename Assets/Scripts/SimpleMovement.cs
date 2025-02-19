using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleMovement : MonoBehaviour
{
    private int moveSpeed = 8;
    private Rigidbody2D rb;
    private Vector2 moveDirection;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Rigidbody2D ophalen
    }

    // Update is called once per frame

    // Input verwerken
    void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        // Controleer of er input is
        if (moveX != 0 || moveY != 0)
        {
            moveDirection = new Vector2(moveX, moveY).normalized;
        }
        else
        {
            moveDirection = Vector2.zero; // Stop beweging als er geen input is
        }
    }


    void FixedUpdate()
    {
        rb.velocity = moveDirection * moveSpeed;
    }

}
