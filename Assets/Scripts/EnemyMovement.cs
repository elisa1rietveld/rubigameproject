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
    private bool isFrozen = false; // Flag to track if the enemy is frozen

    private void Start()
    {
        if (player == null)
        {
            Debug.LogError("Player transform is not assigned!");
        }

        spriteRenderer = GetComponent<SpriteRenderer>();

        SetRandomTargetPosition();
    }

    private void Update()
    {
        // If the enemy is frozen, don't allow it to move
        if (isFrozen)
        {
            return;
        }

        // If the enemy is following the player, update the target position
        if (spriteRenderer.sprite == firstSprite)
        {
            if (isFollowingPlayer)
            {
                targetPosition = player.position;
            }
        }
        else if (spriteRenderer.sprite == secondSprite)
        {
            isFollowingPlayer = false;
        }

        // Move towards the target position
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        // Change direction or set a new target when the current target is reached
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

    // Method to freeze the enemy
    public void FreezeEnemy()
    {
        isFrozen = true; // Set the freeze flag
    }

    // Method to restore the enemy (unfreeze it)
    public void UnfreezeEnemy()
    {
        isFrozen = false; // Reset the freeze flag
    }
}