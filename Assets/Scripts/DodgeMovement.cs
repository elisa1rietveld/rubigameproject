using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DodgeMovement : MonoBehaviour
{
    public float dodgeSpeed = 5f; //speed of the dodge
    public float dodgeTime = 0.5f; // duration of the dodge
    public float dodgeCooldown = 1f; // cooldown between dodges
    public bool isInvulnerableDuringDodge = true; // is the player invulnerable during the dodge
    

    private Rigidbody2D rb;
    private bool isDodging = false;
    private float dodgeCooldownTimer = 0f;
    private Vector2 dodgeDirection;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); //find the rigidbody2d component.
    }

    // Update is called once per frame
    void Update()
    {
        // cooldown update
        if (dodgeCooldownTimer > 0)
        {
            dodgeCooldownTimer -= Time.deltaTime;
        }

        // check if the player is pressing Q (left dodge) or E (right dodge)
        if (!isDodging && dodgeCooldownTimer <= 0)
        {
            if (Input.GetKeyDown(KeyCode.Q)) // Dodge naar links
            {
                dodgeDirection = Vector2.left; // set dodge direction to left
                TryDodge();
                Debug.Log("Dodge naar links");
            }
            else if (Input.GetKeyDown(KeyCode.Z)) // Dodge naar rechts
            {
                dodgeDirection = Vector2.right; // set dodge direction to right
                TryDodge();
                Debug.Log("Dodge naar rechts");
            }
        }
    }

    void TryDodge()
    {
        StartCoroutine(PerformDodge()); // start the dodge coroutine
    }
    IEnumerator PerformDodge()
    {
        isDodging = true;
        dodgeCooldownTimer = dodgeCooldown;

        if (isInvulnerableDuringDodge)
        {
            GetComponent<BoxCollider2D>().enabled = false; // disable the collider
        }
        
        float dodgeDistance = dodgeSpeed * dodgeTime; // calculate the distance of the dodge
        Vector2 startPosition = rb.position; // get the start position of the dodge
        Vector2 targetPosition = startPosition + dodgeDirection * dodgeDistance; // calculate the target position of the dodge

        float elapsedTime = 0f; // set the elapsed time to 0
        
        while (elapsedTime < dodgeTime) // while the elapsed time is less than the dodge time
        {
            rb.position = Vector2.Lerp(startPosition, targetPosition, elapsedTime / dodgeTime); // move the player to the target position
            elapsedTime += Time.deltaTime; // increment the elapsed time
            yield return null; // wait for the next frame
        }

        rb.position = targetPosition; // set the player to the target position

        isDodging = false; // set the player to not dodging

        //reset the collider(if it was disabled)
        if (isInvulnerableDuringDodge)
        {
            GetComponent<BoxCollider2D>().enabled = true;
        }

        Debug.Log("Dodge ended. Dodged distance: " + " in direction: " + dodgeDirection);

    }
}
