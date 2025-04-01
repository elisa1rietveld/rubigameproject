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
    public Sprite terminalHackedSprite; 
    public float hackRange = 2f; 
    private Transform player; 
    private bool isEnemyFrozen = false; 
    private float freezeTimer = 0f; 
    private bool isHacked = false; 

    private SpriteRenderer terminalSpriteRenderer; 
    private Animator enemyAnimator;

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        terminalSpriteRenderer = GetComponent<SpriteRenderer>(); 
        enemyAnimator = enemy.GetComponent<Animator>(); 
    }

    void Update()
    {
        if (isHackingInProgress && !isHacked) 
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

        if (Vector3.Distance(player.position, transform.position) <= hackRange && !isHacked) 
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
        
       
        enemyAnimator.enabled = false; 
        enemy.GetComponent<EnemyPatrol>().FreezeEnemy(); 
        freezeTimer = 0f; 
        isHacked = true; 
        
       
        if (terminalSpriteRenderer != null) 
        {
            terminalSpriteRenderer.sprite = terminalHackedSprite;
        }

    }

    void RestoreEnemy()
    {
        isEnemyFrozen = false;
        
     
        enemy.GetComponent<SpriteRenderer>().sprite = originalSprite; 
        
   
        enemyAnimator.enabled = true;
        
        enemy.GetComponent<EnemyPatrol>().UnfreezeEnemy(); 
        freezeTimer = 0f; 

    }
}
