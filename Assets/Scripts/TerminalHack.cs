using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TerminalHack : MonoBehaviour
{
    public Slider hackSlider; // Reference to your hacking slider
    public static bool isHacking = false; // Track if hacking is active
    private bool isHackingInProgress = false; // Track if hacking is in progress
    private float hackProgress = 0f; // Track progress of hacking
    public GameObject enemy; // Reference to the enemy object
    public Sprite hackedSprite; // Sprite to show when enemy is hacked
    public Sprite originalSprite; // Original sprite of the enemy
    public float hackRange = 2f; // Range to interact with the terminal
    private Transform player; // Reference to the player
    private bool isEnemyFrozen = false; // To check if the enemy is frozen
    private float freezeTimer = 0f; // Timer to keep track of the 5 seconds freeze

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
    }

    void Update()
    {
        if (isHackingInProgress)
        {
            hackProgress += Time.deltaTime / 5f; // Adjust this to control the hack speed
            hackSlider.value = hackProgress;

            if (hackProgress >= 1f) // When the hacking is complete
            {
                CompleteHack();
            }
        }

        if (isEnemyFrozen)
        {
            freezeTimer += Time.deltaTime;

            if (freezeTimer >= 5f) // 5 seconds freeze duration
            {
                RestoreEnemy(); // Restore enemy after 5 seconds
            }
        }

        // Check if the player is within range to interact with the terminal and if "E" is pressed
        if (Vector3.Distance(player.position, transform.position) <= hackRange && !isHackingInProgress)
        {
            if (Input.GetKeyDown(KeyCode.E)) // Only press "E" when near the terminal
            {
                StartHack();
            }
        }
    }

    // Start the hacking process
    void StartHack()
    {
        isHackingInProgress = true;
        hackSlider.gameObject.SetActive(true); // Show the slider
        hackProgress = 0f; // Reset progress
    }

    // When hacking is complete, freeze the enemy and change its sprite
    void CompleteHack()
    {
        hackSlider.gameObject.SetActive(false); // Hide the slider
        isHacking = true;
        isHackingInProgress = false;
        isEnemyFrozen = true; // Freeze the enemy
        enemy.GetComponent<SpriteRenderer>().sprite = hackedSprite; // Change sprite to hacked one
        enemy.GetComponent<EnemyMovement>().FreezeEnemy(); // Freeze enemy movement
        freezeTimer = 0f; // Reset the freeze timer
        Debug.Log("Terminal Hacked! Enemy frozen for 5 seconds.");
    }

    // Restore the enemy (after the 5 seconds)
    void RestoreEnemy()
    {
        isEnemyFrozen = false;
        enemy.GetComponent<SpriteRenderer>().sprite = originalSprite; // Revert the sprite to the original
        enemy.GetComponent<EnemyMovement>().UnfreezeEnemy(); // Unfreeze the enemy movement
        freezeTimer = 0f; // Reset the freeze timer
        Debug.Log("Enemy restored.");
    }
}
