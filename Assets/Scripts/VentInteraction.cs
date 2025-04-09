using UnityEngine;

public class VentInteraction : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private float activationDistance = 2.5f;
    [SerializeField] private Animator ventAnimator;
    [SerializeField] private GameObject player; // Sleep je player hierheen in Inspector

    private bool isInVentMode = false;
    private PlayerMovement playerMovement; // Reference zonder aanpassingen

    private void Start()
    {
        // Safe reference zonder component aan te passen
        playerMovement = player.GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        float distance = Vector3.Distance(transform.position, player.transform.position);

        // Update vent animation
        ventAnimator?.SetBool("IsOpen", distance <= activationDistance);

        // Handle vent mode toggle
        if (Input.GetKeyDown(KeyCode.Space) && distance <= activationDistance)
        {
            ToggleVentMode();
        }
    }

    private void ToggleVentMode()
    {
        isInVentMode = !isInVentMode;

        // Tijdelijke disable van movement tijdens vent mode
        if (playerMovement != null)
        {
            playerMovement.enabled = !isInVentMode;
        }

        if (isInVentMode)
        {
            VentCameraManager.Instance.ActivateOverviewCamera();
        }
        else
        {
            VentCameraManager.Instance.ActivateMainCamera();
        }
    }
}