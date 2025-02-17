using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kogel : MonoBehaviour
{
    public float speed = 10f;
    private Vector3 target;

    void Start()
    {
        target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        target.z = 0;

        // Log the initial target position for the bullet
        Debug.Log("Bullet target position set to: " + target);
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

        // Log the current position of the bullet each frame
        Debug.Log("Bullet position: " + transform.position);

        if (Vector3.Distance(transform.position, target) < 0.2f)  // Increased tolerance
        {
            // Log that the bullet is about to be destroyed
            Debug.Log("Bullet reached target, destroying...");
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Log the object the bullet collided with
        Debug.Log("Bullet collided with: " + collision.gameObject.name);

        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Log the specific enemy hit
            Debug.Log("Bullet hit the enemy!");

            EnemyMovement enemy = collision.gameObject.GetComponent<EnemyMovement>();
            if (enemy != null)
            {
                // Log freezing the enemy
                Debug.Log("Freezing enemy...");
                enemy.FreezeEnemy();
            }

            // Log bullet destruction after collision
            Debug.Log("Destroying bullet after collision...");
            Destroy(gameObject);
        }
    }
}
