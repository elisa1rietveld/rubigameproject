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

    public float maxHealth = 150f;
    public float maxSpeed = 7f;
    public float maxArmor = 50f;

    public TMP_Text pointsText;
    public TMP_Text healthText;
    public TMP_Text speedText;
    public TMP_Text armorText;

    private PlayerMovement playerMovement;

    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerMovement.speed = playerSpeed; // make sure it's synced at start
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
            return;
        }

        switch (type)
        {
            case UpgradeType.Health:
                playerHealth = Mathf.Min(playerHealth + 20f, maxHealth);
                break;
            case UpgradeType.Speed:
                playerSpeed = Mathf.Min(playerSpeed + 1f, maxSpeed);
                playerMovement.speed = playerSpeed; // keep movement synced
                break;
            case UpgradeType.Armor:
                playerArmor = Mathf.Min(playerArmor + 5f, maxArmor);
                break;
        }

        hackPoints -= upgradeCost;
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (!pointsText || !healthText || !speedText || !armorText) return;

        pointsText.SetText($"Hack Points: {hackPoints}");
        healthText.SetText($"Health: {playerHealth} / {maxHealth}");
        speedText.SetText($"Speed: {playerSpeed} / {maxSpeed}");
        armorText.SetText($"Armor: {playerArmor} / {maxArmor}");
    }
}
