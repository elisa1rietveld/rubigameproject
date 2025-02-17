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
    public float hackRange = 2f; 
    private Transform player; 
    private bool isEnemyFrozen = false; 
    private float freezeTimer = 0f; 

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
    }

    void Update()
    {
        if (isHackingInProgress)
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

        if (Vector3.Distance(player.position, transform.position) <= hackRange && !isHackingInProgress)
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
        enemy.GetComponent<EnemyMovement>().FreezeEnemy(); 
        freezeTimer = 0f; 
        Debug.Log("Terminal Hacked! Enemy frozen for 5 seconds.");
    }

   
    void RestoreEnemy()
    {
        isEnemyFrozen = false;
        enemy.GetComponent<SpriteRenderer>().sprite = originalSprite; 
        enemy.GetComponent<EnemyMovement>().UnfreezeEnemy(); 
        freezeTimer = 0f; 
        Debug.Log("Enemy restored.");
    }
}
