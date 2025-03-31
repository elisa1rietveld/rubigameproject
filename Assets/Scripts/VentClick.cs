using UnityEngine;

public class VentClick : MonoBehaviour
{
    [SerializeField] private int ventID;
    private VentSystem ventSystem;

    private void Awake()
    {
        ventSystem = FindObjectOfType<VentSystem>();
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
