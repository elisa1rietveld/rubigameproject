using UnityEngine;
using System.Collections;

public class EnemyPatrol : MonoBehaviour
{
    public Transform[] patrolPoints;
    public float speed = 90f;
    private int currentPointIndex;

    public Transform player;
    public float detectionRadius = 300f;
    private bool isChasing;
    private bool playerIsHidden;
    private bool isWaiting;

    void Start()
    {
        currentPointIndex = 0;
        transform.position = patrolPoints[currentPointIndex].position;
    }

    void Update()
    {
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
        if (Vector2.Distance(transform.position, patrolPoints[currentPointIndex].position) < 0.1f)
        {
            StartCoroutine(WaitAtWaypoint());
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, patrolPoints[currentPointIndex].position, speed * Time.deltaTime);
        }
    }

    IEnumerator WaitAtWaypoint()
    {
        isWaiting = true;
        yield return new WaitForSeconds(1f);
        currentPointIndex = (currentPointIndex + 1) % patrolPoints.Length;
        isWaiting = false;
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

    public void SetPlayerHidden(bool hidden)
    {
        playerIsHidden = hidden;
        if (hidden)
        {
            isChasing = false;
        }
    }

    void FollowPlayer()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
