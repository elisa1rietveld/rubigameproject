using UnityEngine;

public class Vent : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float openDistance = 2.5f;
    [SerializeField] private Animator ventAnimator;
    [SerializeField] private GameObject ventObject; // Referentie naar het vent object met animator

    private bool isInVentMode = false;

    private void Start()
    {
        if (ventAnimator == null && ventObject != null)
        {
            ventAnimator = ventObject.GetComponent<Animator>();
        }
    }

    private void Update()
    {
        HandleVentAnimation();
        HandleVentInput();
    }

    private void HandleVentAnimation()
    {
        bool isPlayerNear = Vector3.Distance(VentSystem.Instance.Player.position, transform.position) <= openDistance;

        if (ventAnimator != null)
        {
            ventAnimator.SetBool("IsOpen", isPlayerNear);
        }
        else
        {
            Debug.LogWarning("Vent animator is not assigned!");
        }
    }

    private void HandleVentInput()
    {
        if (Input.GetKeyDown(KeyCode.Space) && IsPlayerNearVent())
        {
            ToggleVentMode();
        }
    }

    private bool IsPlayerNearVent()
    {
        return Vector3.Distance(VentSystem.Instance.Player.position, transform.position) <= openDistance;
    }

    private void ToggleVentMode()
    {
        isInVentMode = !isInVentMode;

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