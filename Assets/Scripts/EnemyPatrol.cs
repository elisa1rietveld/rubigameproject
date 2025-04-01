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
    private Animator animator; 
    private bool isFrozen = false;
    private bool isChasing;
    private bool playerIsHidden;
    private bool isWaiting;

    private bool isHacked = false; 
    private bool hasReachedTarget = false;

    public Transform point3; 

    private void Start()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }

        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>(); 
        currentPatrolPointIndex = 0;
        transform.position = patrolPoints[currentPatrolPointIndex].position;
    }

    private void Update()
    {
        if (isFrozen || isHacked) 
        {
            if (isHacked && !hasReachedTarget)
            {
                MoveToPoint3(); 
            }
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

    void MoveToPoint3()
    {
        if (point3 != null) // Ensure Point 3 is assigned
        {
            transform.position = Vector2.MoveTowards(transform.position, point3.position, patrolSpeed * Time.deltaTime);

            if (Vector2.Distance(transform.position, point3.position) < 0.1f)
            {
                hasReachedTarget = true; // Stop once Point 3 is reached
            }
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

    public void StopFollowingPlayer()
    {
        isChasing = false;
    }

    public void HackEnemy()
    {
        isHacked = true;
        StopFollowingPlayer(); 
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
        animator.enabled = false; 
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
        animator.enabled = true; 
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

    public void SetPlayerHidden(bool hidden)
    {
        playerIsHidden = hidden;
        if (hidden)
        {
            isChasing = false;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
