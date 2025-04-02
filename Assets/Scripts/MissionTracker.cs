using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MissionTracker : MonoBehaviour
{
    [Header("Mission Settings")]
    [SerializeField] private string missionDescription = "Hack 3 robots";
    [SerializeField] private int maxRobotsToHack = 3;
    private int robotsHacked = 0;

    [Header("UI References")]
    [SerializeField] private TextMeshProUGUI missionText; // UI Text om voortgang te tonen

    [Header("Debug Settings")]
    [SerializeField] private bool debugMode = true; // Debug aan/uit
    [SerializeField] private KeyCode debugKey = KeyCode.T; // Sneltoets om hacking te simuleren

    private void Start()
    {
        UpdateMissionUI(); // Initialiseer UI
    }

    private void Update()
    {
        // DEBUG: Simuleer robot hacken met sneltoets (alleen in debugmode)
        if (debugMode && Input.GetKeyDown(debugKey))
        {
            IncrementRobotsHacked();
        }
    }

    /// <summary>
    /// Verhoog het aantal gehackte robots en update UI.
    /// Roept MissionComplete() aan als de missie voltooid is.
    /// </summary>
    public void IncrementRobotsHacked()
    {
        if (robotsHacked >= maxRobotsToHack) return; // Stop als missie al klaar is

        robotsHacked++;
        UpdateMissionUI();

        // Controleer of de missie voltooid is
        if (robotsHacked >= maxRobotsToHack)
        {
            MissionComplete();
        }
    }

    /// <summary>
    /// Update de missie-text in de UI (bijv. "1/3 robots gehackt").
    /// </summary>
    private void UpdateMissionUI()
    {
        if (missionText != null)
        {
            missionText.text = $"{missionDescription}: {robotsHacked}/{maxRobotsToHack}";
        }
    }

    /// <summary>
    /// Wordt aangeroepen wanneer de missie is voltooid.
    /// </summary>
    private void MissionComplete()
    {
        Debug.Log("Mission Complete: " + missionDescription);
        // Hier kun je rewards, volgende missie, etc. triggeren
    }
}