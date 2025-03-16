using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vent : MonoBehaviour
{
    public GameObject teleportation; // Het lege gameobject waar de speler naartoe wordt geteleporteerd
    public Transform teleport; // De positie waar de speler naartoe wordt geteleporteerd
    private Animator ventAnimator; // Referentie naar de Animator component
    public GameObject VentLocation;
    public float openDistance = 2.5f;
    public Camera mainCamera;
    public Camera secondCamera;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {

        ventAnimator = GetComponentInChildren<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        mainCamera.enabled = true;
        secondCamera.enabled = false;

    }

    void Update()
    {
        if (player != null)
        {
            // Bereken de afstand tussen de vent en de speler
            float distance = Vector2.Distance(transform.position, player.transform.position);

            // Als de speler dichtbij genoeg is, open de vent
            if (distance <= openDistance)
            {
                Debug.Log("Player is within open distance. Opening vent.");
                ventAnimator.SetBool("IsOpen", true);

                // Controleer of de speler op de K-toets drukt om te teleporteren
                if (Input.GetKeyDown(KeyCode.K))
                {
                    Debug.Log("K key pressed.");
                    // Teleporteer de speler naar de teleport positie
                    if (VentLocation != null)
                    {
                        Debug.Log("Teleporting player to VentLocation.");
                        player.transform.position = VentLocation.transform.position;

                        // Schakel de camera's
                        if (mainCamera != null && secondCamera != null)
                        {
                            mainCamera.enabled = false;
                            secondCamera.enabled = true;
                        }
                    }
                    else
                    {
                        Debug.LogWarning("VentLocation is not assigned.");
                    }
                }
            }
            else
            {
                // Als de speler te ver weg is, sluit de vent
                Debug.Log("Player is too far. Closing vent.");
                ventAnimator.SetBool("IsOpen", false);
            }
        }
    }
}