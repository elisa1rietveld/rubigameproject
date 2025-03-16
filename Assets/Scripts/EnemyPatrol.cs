using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{

    public float moveSpeed = 2f;
    public Transform player;
    public Sprite firstSprite;
    public Sprite secondSprite;
    public Transform[] patrolPoints;
    public float patrolSpeed = 90f;
    public float detectionRadius = 300f;
    
    private int currentPatrolPointIndex;
    private SpriteRenderer spriteRenderer;
    private bool isFrozen = false;
    private bool isChasing;
    private bool playerIsHidden;
    private bool isWaiting;

    private void Start()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }

        spriteRenderer = GetComponent<SpriteRenderer>();
        currentPatrolPointIndex = 0;
        transform.position = patrolPoints[currentPatrolPointIndex].position;
    }

    private void Update()
    {
        if (isFrozen)
        {
            return;
        }

        if (playerIsHidden)
        {
            isChasing = false;
        }
        else
        {
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);

            if (distanceToPlayer <= detectionRadius)
            {
                isChasing = true;
            }
            else
            {
                isChasing = false;
            }
        }

        if (isChasing)
        {
            StopAllCoroutines();
            isWaiting = false;
            FollowPlayer();
        }
        else if (!isWaiting)
        {
            Patrol();
        }
    }

    void Patrol()
    {
        if (Vector2.Distance(transform.position, patrolPoints[currentPatrolPointIndex].position) < 0.1f)
        {
            StartCoroutine(WaitAtWaypoint());
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, patrolPoints[currentPatrolPointIndex].position, patrolSpeed * Time.deltaTime);
        }
    }

    IEnumerator WaitAtWaypoint()
    {
        isWaiting = true;
        yield return new WaitForSeconds(1f);
        currentPatrolPointIndex = (currentPatrolPointIndex + 1) % patrolPoints.Length;
        isWaiting = false;
    }

    void FollowPlayer()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.position, patrolSpeed * Time.deltaTime);
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
            EnemyPatrol enemy = collision.gameObject.GetComponent<EnemyPatrol>();
            if (enemy != null)
            {
                enemy.FreezeEnemy();
            }

            Destroy(gameObject);
        }
    }

    // Control visibility of player for chasing logic
    public void SetPlayerHidden(bool hidden)
    {
        playerIsHidden = hidden;
        if (hidden)
        {
            isChasing = false;
        }
    }

    public void StopFollowingPlayer()
    {
        isChasing = false;
    }

    public void StartFollowingPlayer()
    {
        if (!playerIsHidden)
        {
            isChasing = true;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
