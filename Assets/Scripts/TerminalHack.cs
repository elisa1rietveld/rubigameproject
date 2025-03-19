using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TerminalHack : MonoBehaviour
{
    public Slider hackSlider; 
    public static bool isHacking = false; 
    private bool isHackingInProgress = false; 
    private float hackProgress = 0f; 
    public GameObject enemy; 
    public Sprite hackedSprite; 
    public Sprite originalSprite; 
    public Sprite terminalHackedSprite; // New sprite for the terminal
    public float hackRange = 2f; 
    private Transform player; 
    private bool isEnemyFrozen = false; 
    private float freezeTimer = 0f; 
    private bool isHacked = false; // Flag to check if already hacked

    private SpriteRenderer terminalSpriteRenderer; // Reference to the terminal's SpriteRenderer

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        terminalSpriteRenderer = GetComponent<SpriteRenderer>(); // Get the SpriteRenderer of the terminal
    }

    void Update()
    {
        if (isHackingInProgress && !isHacked) // Only allow hacking if it's not already hacked
        {
            hackProgress += Time.deltaTime / 5f; 
            hackSlider.value = hackProgress;

            if (hackProgress >= 1f) 
            {
                CompleteHack();
            }
        }

        if (isEnemyFrozen)
        {
            freezeTimer += Time.deltaTime;

            if (freezeTimer >= 5f) 
            {
                RestoreEnemy(); 
            }
        }

        if (Vector3.Distance(player.position, transform.position) <= hackRange && !isHacked) // Prevent hacking if already hacked
        {
            if (Input.GetKeyDown(KeyCode.E)) 
            {
                StartHack();
            }
        }
    }

    void StartHack()
    {
        isHackingInProgress = true;
        hackSlider.gameObject.SetActive(true); 
        hackProgress = 0f; 
    }

    void CompleteHack()
    {
        hackSlider.gameObject.SetActive(false); 
        isHacking = true;
        isHackingInProgress = false;
        isEnemyFrozen = true; 
        enemy.GetComponent<SpriteRenderer>().sprite = hackedSprite; 
        enemy.GetComponent<EnemyPatrol>().FreezeEnemy(); 
        freezeTimer = 0f; 
        isHacked = true; // Set the flag to prevent future hacks
        
        // Change the terminal sprite to indicate it's hacked
        if (terminalSpriteRenderer != null) 
        {
            terminalSpriteRenderer.sprite = terminalHackedSprite;
        }
        
        Debug.Log("Terminal Hacked! Enemy frozen for 5 seconds.");
    }

    void RestoreEnemy()
    {
        isEnemyFrozen = false;
        enemy.GetComponent<SpriteRenderer>().sprite = originalSprite; 
        enemy.GetComponent<EnemyPatrol>().UnfreezeEnemy(); 
        freezeTimer = 0f; 
        Debug.Log("Enemy restored.");
    }
}
