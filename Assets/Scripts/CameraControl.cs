using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour

{
    
    public Vector3 fixedPosition;
    public bool isCameraActive = false;  

    void Start()
    {
        gameObject.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C) && ElectricityBoxInteraction.isHacking)
        {
            ToggleCamera();
        }
    }

    private void ToggleCamera()
    {
        isCameraActive = !isCameraActive;  
        gameObject.SetActive(isCameraActive);  
        if (isCameraActive)
        {
            transform.position = fixedPosition;  
            Debug.Log("Camera is now active and fixed in position.");
        }
        else
        {
            Debug.Log("Camera is deactivated.");
        }
    }
}
