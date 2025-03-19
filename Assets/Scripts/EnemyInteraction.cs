using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EnemyInteraction : MonoBehaviour
{
    public Slider slider;
    private bool isInteracting = false;
    public float interactionRange = 2f;
    private bool playerInRange = false;
    private bool hasHacked = false;
    private EnemyMovement enemyMovement;
    private Animator enemyAnimator;

    void Start()
    {
        enemyMovement = GetComponent<EnemyMovement>();
        enemyAnimator = GetComponent<Animator>();

        if (enemyAnimator == null)
        {
            Debug.LogError("No Animator found on the enemy GameObject!");
            return;
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
