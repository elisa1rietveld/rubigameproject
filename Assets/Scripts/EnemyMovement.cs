using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float moveSpeed = 2f;
    public Transform player;
    public Sprite firstSprite;
    public Sprite secondSprite;

    private SpriteRenderer spriteRenderer;
    private bool isFrozen = false;

    private void Start()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }

        spriteRenderer = GetComponent<SpriteRenderer>();
      
    }

    private void Update()
    {
        if (isFrozen)
        {
            return;
        }


    }

    public void FreezeEnemy()
    {
        isFrozen = true;
        ChangeToSecondSprite();
        StartCoroutine(UnfreezeAfterDelay(3f));
    }

    private IEnumerator UnfreezeAfterDelay(float freezeTime)
    {
        yield return new WaitForSeconds(freezeTime);
        UnfreezeEnemy();
    }

    public void UnfreezeEnemy()
    {
        isFrozen = false;
        spriteRenderer.sprite = firstSprite;

    }

    public void ChangeToSecondSprite()
    {
        spriteRenderer.sprite = secondSprite;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            EnemyMovement enemy = collision.gameObject.GetComponent<EnemyMovement>();
            if (enemy != null)
            {
                enemy.FreezeEnemy();
            }

            Destroy(gameObject);
        }
    }
}
