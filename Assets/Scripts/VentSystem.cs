using UnityEngine;
using System.Collections.Generic;

public class VentSystem : MonoBehaviour
{
    public static VentSystem Instance { get; private set; }

    [Header("Player References")]
    public Transform Player;
    public Transform ReturnLocation;

    [Header("Vent Management")]
    [SerializeField] private List<Transform> ventLocations;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void TeleportToVent(int ventID)
    {
        if (ventID >= 0 && ventID < ventLocations.Count)
        {
            Player.position = ventLocations[ventID].position;
        }
    }
}