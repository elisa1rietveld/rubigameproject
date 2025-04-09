using UnityEngine;

public class VentClick : MonoBehaviour
{
    [SerializeField] private int ventID;
    private VentSystem ventSystem;

    private void Awake()
    {
        ventSystem = FindObjectOfType<VentSystem>();
        if (ventSystem == null)
        {
            Debug.LogError("VentSystem not found in the scene.");
        }
    }

    private void OnMouseDown()
    {
        if (VentCameraManager.Instance.OverviewCamera.enabled)
        {
            ventSystem.TeleportToVent(ventID);
            VentCameraManager.Instance.ActivateVentCamera(ventID);
        }
    }
}
