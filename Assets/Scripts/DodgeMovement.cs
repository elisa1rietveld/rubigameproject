using System.Collections;
using TMPro;
using UnityEngine;

public class DodgeMovement : MonoBehaviour
{
    private float dodgeSpeed = 5f; // Speed of the dodge
    private float dodgeTime = 0.5f; // Duration of the dodge
    private float dodgeCooldown = 5f; // Cooldown between dodges

    public TextMeshProUGUI cooldownText;
    private Rigidbody2D rb;
    private bool isDodging = false;
    private float dodgeCooldownTimer = 0f;
    private Vector2 dodgeDirection;

    // Double-tap variables
    private float doubleTapTime = 0.3f; // Max time allowed between double taps
    private float lastATapTime = -1f;
    private float lastDTapTime = -1f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Find the Rigidbody2D component
        if (cooldownText != null)
        {
            cooldownText.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        // Update cooldown timer
        if (dodgeCooldownTimer > 0)
        {
            dodgeCooldownTimer -= Time.deltaTime;

            if (cooldownText != null)
            {
                cooldownText.gameObject.SetActive(true);
                cooldownText.text = "Cooldown: " + Mathf.Ceil(dodgeCooldownTimer).ToString() + "S";
            }
        }
        else
        {
            if (cooldownText != null)
            {
                cooldownText.gameObject.SetActive(false);
            }
        }

        // Check for double-tap input
        if (!isDodging && dodgeCooldownTimer <= 0)
        {
            HandleDoubleTap();
        }
    }

    // Handles double-tap detection for A and D keys
    void HandleDoubleTap()
    {
        // Check for 'A' double-tap to dodge left
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (Time.time - lastATapTime <= doubleTapTime)
            {
                dodgeDirection = Vector2.left; // Set dodge direction to left
                TryDodge();
                Debug.Log("Dodge Left");
                lastATapTime = -1f; // Reset tap time
            }
            else
            {
                lastATapTime = Time.time; // Register first tap
            }
        }

        // Check for 'D' double-tap to dodge right
        if (Input.GetKeyDown(KeyCode.D))
        {
            if (Time.time - lastDTapTime <= doubleTapTime)
            {
                dodgeDirection = Vector2.right; // Set dodge direction to right
                TryDodge();
                Debug.Log("Dodge Right");
                lastDTapTime = -1f; // Reset tap time
            }
            else
            {
                lastDTapTime = Time.time; // Register first tap
            }
        }
    }

    // Starts the dodge coroutine
    void TryDodge()
    {
        StartCoroutine(PerformDodge());
    }

    // Performs the dodge movement over time
    IEnumerator PerformDodge()
    {
        isDodging = true;
        dodgeCooldownTimer = dodgeCooldown;

        float dodgeDistance = dodgeSpeed * dodgeTime; // Calculate total dodge distance
        Vector2 startPosition = rb.position; // Start position of the dodge
        Vector2 targetPosition = startPosition + dodgeDirection * dodgeDistance; // Target position

        float elapsedTime = 0f;

        // Move the player towards the target position during the dodge time
        while (elapsedTime < dodgeTime)
        {
            rb.position = Vector2.Lerp(startPosition, targetPosition, elapsedTime / dodgeTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        rb.position = targetPosition; // Snap player to the target position
        isDodging = false;

        Debug.Log("Dodge ended. Dodged distance: " + dodgeDistance + " in direction: " + dodgeDirection);
    }
}
