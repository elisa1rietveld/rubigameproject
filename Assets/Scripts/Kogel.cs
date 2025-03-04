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

        
        Debug.Log("Bullet target position set to: " + target);
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);




        if (Vector3.Distance(transform.position, target) < 0.2f)  
        {

            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {

        Debug.Log("Bullet collided with: " + collision.gameObject.name);

        if (collision.gameObject.CompareTag("Enemy"))
        {

            Debug.Log("Bullet hit the enemy!");

            EnemyPatrol enemy = collision.gameObject.GetComponent<EnemyPatrol>();
            if (enemy != null)
            {

                Debug.Log("Freezing enemy...");
                enemy.FreezeEnemy();
            }

            Debug.Log("Destroying bullet after collision...");
            Destroy(gameObject);
        }
    }
}
