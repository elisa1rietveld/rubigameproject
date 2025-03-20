using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EnemyInteraction : MonoBehaviour
{
    public Slider slider;
    public Sprite badEnemySprite;
    public Sprite changedEnemySprite;
    private SpriteRenderer enemyRenderer;
    private bool isInteracting = false;
    public float interactionRange = 2f;
    private bool playerInRange = false;
    private EnemyMovement enemyMovement;
    private Animator enemyAnimator;
    private bool hasHacked = false;

    private PlayerUpgradeSystem playerUpgradeSystem;

    void Start()
    {
        enemyMovement = GetComponent<EnemyMovement>();
        enemyAnimator = GetComponent<Animator>();

        if (enemyAnimator == null)
        {
            Debug.LogError("No Animator found on the enemy GameObject!");
            return;
        }

        enemyRenderer.sprite = badEnemySprite;

        // Find the PlayerUpgradeSystem properly
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            playerUpgradeSystem = player.GetComponent<PlayerUpgradeSystem>();
        }
        else
        {
            Debug.LogError("PlayerUpgradeSystem not found! Ensure the Player has a 'Player' tag.");
        }
    }

    void Update()
    {
        if (!hasHacked && playerInRange && !isInteracting)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                StartCoroutine(StartSliderInteraction());
            }
        }
    }

    IEnumerator StartSliderInteraction()
    {
        slider.gameObject.SetActive(true); 
        slider.value = 0;
        isInteracting = true;

        float timePassed = 0;
        while (timePassed < 5f) 
        {
            timePassed += Time.deltaTime;
            slider.value = timePassed / 5f; 
            yield return null;
        }

        // Trigger the ConvertWalking animation after the hacking process finishes
        enemyAnimator.SetTrigger("ConvertWalking");

        slider.gameObject.SetActive(false); 
        isInteracting = false;
        hasHacked = true;

    

        HackedEnemy(); // Call HackedEnemy() after hacking is complete
        
        // You can perform other post-hack actions like notifying the enemy's patrol to stop
        EnemyPatrol enemyPatrol = GetComponent<EnemyPatrol>();
        if (enemyPatrol != null)
        {
            enemyPatrol.HackEnemy();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true; 
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false; 
        }
    }


    void HackedEnemy()
    {
        if (playerUpgradeSystem != null)
        {
            playerUpgradeSystem.AddHackPoint();
        }
        else
        {
            Debug.LogError("PlayerUpgradeSystem is null! Cannot add hack points.");
        }
    }


    // This method can be called to trigger the freeze animation when the enemy is frozen
    public void FreezeEnemy()
    {
        enemyAnimator.SetBool("IsFrozen", true);  // Assuming you have a frozen animation in the Animator
    }

    // This method will be called to unfreeze the enemy and revert its animation
    public void UnfreezeEnemy()
    {
        enemyAnimator.SetBool("IsFrozen", false); // Reverts to normal animation after unfreeze

    }
}
