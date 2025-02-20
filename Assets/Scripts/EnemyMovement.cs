using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float changeDirectionTime = 2f;
    public Transform player;
    public Sprite firstSprite;
    public Sprite secondSprite;

    private Vector3 targetPosition;
    private float timer;
    private bool isFollowingPlayer = false;
    private SpriteRenderer spriteRenderer;
    private bool isFrozen = false;

    private void Start()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }

        spriteRenderer = GetComponent<SpriteRenderer>();
        isFollowingPlayer = true;
        SetRandomTargetPosition();
    }

    private void Update()
    {
        if (isFrozen)
        {
            return;
        }

        if (isFollowingPlayer)
        {
            targetPosition = player.position;
        }

        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            if (!isFollowingPlayer)
            {
                if (timer >= changeDirectionTime)
                {
                    SetRandomTargetPosition();
                    timer = 0f;
                }
                else
                {
                    timer += Time.deltaTime;
                }
            }
        }
    }

    private void SetRandomTargetPosition()
    {
        float randomX = Random.Range(-5f, 5f);
        float randomY = Random.Range(-5f, 5f);
        targetPosition = new Vector3(randomX, randomY, transform.position.z);
    }

    public void StartFollowingPlayer()
    {
        if (spriteRenderer.sprite == firstSprite)
        {
            isFollowingPlayer = true;
        }
    }

    public void StopFollowingPlayer()
    {
        isFollowingPlayer = false;
        SetRandomTargetPosition();
    }

    public void ChangeToSecondSprite()
    {
        spriteRenderer.sprite = secondSprite;
        StopFollowingPlayer();
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
        SetRandomTargetPosition();
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
