using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kogel : MonoBehaviour

{
    public float bulletSpeed = 10f;
    public Sprite hackedSprite;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            FreezeEnemy(collision.gameObject);
        }

        Destroy(gameObject); 
    }

    void FreezeEnemy(GameObject enemy)
    {
        EnemyMovement enemyMovement = enemy.GetComponent<EnemyMovement>();
        if (enemyMovement != null)
        {
            enemyMovement.FreezeEnemy(); 
        }
    }
}