using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Detector : MonoBehaviour
{
    public bool isSeen;                // Whether the player is seen by the enemy
    public float detectionValue = 0;   // Detection value (0-100)
    public Slider slider;              // Slider for visual feedback

    void Update()
    {
        // Handle detection based on if the player is seen
        if (slider != null)
        {
            slider.value = detectionValue; // Update slider value with detection level
            UpdateDetectionUI();           // Update UI visuals

            if (isSeen)
            {
                if (detectionValue < 100)
                {
                    IncreaseDetection(10); // Increase detection if the player is seen
                }
            }
            else
            {
                if (detectionValue > 0)
                {
                    IncreaseDetection(-10); // Decrease detection if the player is not seen
                }
            }
        }
    }

    private void IncreaseDetection(float x)
    {
        detectionValue += x * Time.deltaTime; // Increase or decrease detection value over time
    }

    private void UpdateDetectionUI()
    {
        // Update the color or other UI aspects based on detection level
        if (detectionValue == 0)
        {
            slider.fillRect.GetComponent<Image>().color = new Color(0, 0, 0, 0); // Transparent when not detected
        }
        else
        {
            slider.fillRect.GetComponent<Image>().color = Color.white; // Visible when detection value is not 0
        }
    }
}
