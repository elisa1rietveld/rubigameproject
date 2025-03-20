using UnityEngine;
using TMPro;

public class PlayerUpgradeSystem : MonoBehaviour
{
    public enum UpgradeType { Health, Speed, Armor }

    public int hackPoints = 0;
    public int upgradeCost = 5;

    public float playerHealth = 100f;
    public float playerSpeed = 5f;
    public float playerArmor = 0f;

    public TMP_Text pointsText;
    public TMP_Text healthText;
    public TMP_Text speedText;
    public TMP_Text armorText;

    private PlayerMovement playerMovement;

    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        UpdateUI();
    }

    public void AddHackPoint()
    {
        hackPoints++;
        UpdateUI();
    }

    public void Upgrade(UpgradeType type)
    {
        if (hackPoints < upgradeCost)
        {
            Debug.Log("Not enough hack points to upgrade.");
            return;
        }

        switch (type)
        {
            case UpgradeType.Health:
                playerHealth += 20f;
                break;
            case UpgradeType.Speed:
                playerSpeed += 1f;
                playerMovement.speed = playerSpeed;
                break;
            case UpgradeType.Armor:
                playerArmor += 5f;
                break;
        }

        hackPoints -= upgradeCost;
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (pointsText == null || healthText == null || speedText == null || armorText == null)
        {
            Debug.LogError("TMP Text elements not assigned in the Inspector!");
            return;
        }

        pointsText.text = "Hack Points: " + hackPoints;
        healthText.text = "Health: " + playerHealth;
        speedText.text = "Speed: " + playerSpeed;
        armorText.text = "Armor: " + playerArmor;
    }
}
