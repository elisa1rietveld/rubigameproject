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

    // check of the speler is dodging
    if (Input.GetKeyDown(KeyCode.Q))
        {
            TryDodge();
            Debug.Log("Dodge");
        }
    }

    void FixedUpdate()
    {
        if (isDodging)
        {
            rb.velocity = dodgeDirection * dodgeSpeed;
        }
    }

    void TryDodge()
    {
        if (dodgeCooldownTimer <= 0 && !isDodging)
        {
           float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            if (horizontal != 0 || vertical != 0)
            {
                dodgeDirection = new Vector2(horizontal, vertical).normalized;
                StartCoroutine(PerformDodge());
            }
        }
    }
    System.Collections.IEnumerator PerformDodge()
    {
        isDodging = true;
        dodgeCooldownTimer = dodgeCooldown;
        if (isInvulnerableDuringDodge)
        {
            GetComponent<Collider2D>().enabled = false; // disable the collider
        }
        // wait for the dodge time
        yield return new WaitForSeconds(dodgeTime);

        // stop dodging
        isDodging = false;
        rb.velocity = Vector2.zero; // stop moving

        //reset the collider(if it was disabled)
        if (isInvulnerableDuringDodge)
        {
            GetComponent<Collider2D>().enabled = true;
        }

    }
}
