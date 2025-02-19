using System.Collections;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public Transform[] patrolPoints;  // Waypoints for patrolling
    public float patrolSpeed = 2f;    // Speed while patrolling
    public float chaseSpeed = 4f;     // Speed while chasing
    private int currentPointIndex = 0;
    private bool isChasing = false;

    private Transform player;
    private Detector playerDetector;
    private Rigidbody2D rb; // Rigidbody for smooth movement

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();  // Get enemy Rigidbody
        player = GameObject.FindGameObjectWithTag("Player")?.transform;

        if (player != null)
        {
            playerDetector = player.GetComponent<Detector>();
        }
        else
        {
            Debug.LogError("Player not found! Make sure your player has the 'Player' tag.");
        }

        StartCoroutine(Patrol());
    }

    void Update()
    {
        if (playerDetector != null && playerDetector.isSeen)
        {
            isChasing = true;
            StopAllCoroutines(); // Stop patrol when chasing
        }
    }

    IEnumerator Patrol()
    {
        while (!isChasing)
        {
            Transform targetPoint = patrolPoints[currentPointIndex];

            while (Vector2.Distance(transform.position, targetPoint.position) > 0.1f)
            {
                MoveTowards(targetPoint.position, patrolSpeed);
                yield return null;
            }

            yield return new WaitForSeconds(2f); // Wait at patrol point
            currentPointIndex = (currentPointIndex + 1) % patrolPoints.Length; // Loop waypoints
        }
    }

    void MoveTowards(Vector2 target, float speed)
    {
        Vector2 direction = (target - (Vector2)transform.position).normalized;
        rb.velocity = direction * speed; // Move using Rigidbody2D
    }

    void FixedUpdate()
    {
        if (isChasing && player != null)
        {
            MoveTowards(player.position, chaseSpeed);
        }
    }
}
