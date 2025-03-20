using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ElectricityBoxInteraction : MonoBehaviour
{
    public Slider hackSlider;
    public static bool isHacking = false;
    private bool isHackingInProgress = false;
    public GameObject cameraObject;
    public float hackRange = 2f;
    private Transform player;
    private float hackProgress = 0f;
    private bool isHacked = false; // Flag to check if already hacked

    public Sprite hackedSprite; // New sprite for the hacked electricity box
    private SpriteRenderer boxSpriteRenderer; // Reference to the electricity box's SpriteRenderer

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        boxSpriteRenderer = GetComponent<SpriteRenderer>(); // Get the SpriteRenderer of the electricity box
    }

    void Update()
    {
        if (isHacking && Input.GetKeyDown(KeyCode.C))
        {
            ToggleCamera();
        }

        if (isHackingInProgress && !isHacked) // Only allow hacking if it's not already hacked
        {
            hackProgress += Time.deltaTime / 5f;
            hackSlider.value = hackProgress;

            if (hackProgress >= 1f)
            {
                CompleteHack();
            }
        }

        if (Vector3.Distance(player.position, transform.position) <= hackRange && !isHackingInProgress && !isHacked) // Prevent hacking if already hacked
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                StartHack();
            }
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isHackingInProgress && !isHacked)
        {
            if (Vector3.Distance(player.position, transform.position) <= hackRange)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    StartHack();
                }
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
        isHacked = true; // Set the flag to prevent future hacks

        // Change the electricity box sprite to indicate it's hacked
        if (boxSpriteRenderer != null)
        {
            boxSpriteRenderer.sprite = hackedSprite;
        }

        Debug.Log("Electricity box hacked!");
    }

    private void ToggleCamera()
    {
        cameraObject.SetActive(!cameraObject.activeSelf);
    }
}
