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


        if (collision.gameObject.CompareTag("Enemy"))
        {



            EnemyPatrol enemy = collision.gameObject.GetComponent<EnemyPatrol>();
            if (enemy != null)
            {

                enemy.FreezeEnemy();
            }

            Destroy(gameObject);
        }
    }
}