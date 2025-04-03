using System.Collections.Generic;
using UnityEngine;

public class VentCameraManager : MonoBehaviour
{
    public static VentCameraManager Instance { get; private set; }

    [Header("Cameras")]
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Camera overviewCamera;
    [SerializeField] private List<Camera> ventCameras;

    // Voeg deze getter toe
    public Camera OverviewCamera => overviewCamera;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        ActivateMainCamera();
    }

    public void ActivateMainCamera()
    {
        mainCamera.enabled = true;
        overviewCamera.enabled = false;
        DisableAllVentCameras();
    }

    public void ActivateOverviewCamera()
    {
        mainCamera.enabled = false;
        overviewCamera.enabled = true;
        DisableAllVentCameras();
    }

    public void ActivateVentCamera(int index)
    {
        DisableAllVentCameras();
        if (index >= 0 && index < ventCameras.Count)
        {
            ventCameras[index].enabled = true;
        }
    }

    private void DisableAllVentCameras()
    {
        foreach (var cam in ventCameras) cam.enabled = false;
    }
}
