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

    void Start()
    {
        enemyRenderer = GetComponent<SpriteRenderer>();
        enemyMovement = GetComponent<EnemyMovement>();
        enemyAnimator = GetComponent<Animator>();

        if (enemyRenderer == null)
        {
            Debug.LogError("No SpriteRenderer found on the enemy GameObject!");
            return;
        }

        enemyRenderer.sprite = badEnemySprite;
    }

    void Update()
    {
        if (playerInRange && !isInteracting)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                enemyAnimator.SetTrigger("IsInteracting");
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

        enemyRenderer.sprite = changedEnemySprite;
        slider.gameObject.SetActive(false);
        isInteracting = false;
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
}
