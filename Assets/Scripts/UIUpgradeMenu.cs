using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class UIUpgradeMenu : MonoBehaviour
{
    public GameObject upgradeMenu;
    public TMP_Text inGamePointsText;
    public TMP_Text menuPointsText;
    public Button healthButton;
    public Button speedButton;
    public Button armorButton;
    public TMP_Text maxedOutText; // TMP text that will appear and fade out
    public TMP_Text notEnoughPointsText; // TMP text for insufficient points

    private PlayerUpgradeSystem playerUpgradeSystem;

    void Start()
    {
        // Find the PlayerUpgradeSystem
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            playerUpgradeSystem = player.GetComponent<PlayerUpgradeSystem>();
        }

        upgradeMenu.SetActive(false);
        maxedOutText.gameObject.SetActive(false);
        notEnoughPointsText.gameObject.SetActive(false);

        // Setup button listeners
        healthButton.onClick.RemoveAllListeners();
        speedButton.onClick.RemoveAllListeners();
        armorButton.onClick.RemoveAllListeners();

        healthButton.onClick.AddListener(() => Upgrade(PlayerUpgradeSystem.UpgradeType.Health));
        speedButton.onClick.AddListener(() => Upgrade(PlayerUpgradeSystem.UpgradeType.Speed));
        armorButton.onClick.AddListener(() => Upgrade(PlayerUpgradeSystem.UpgradeType.Armor));

        UpdateUI();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.U)) // Press 'U' to open/close menu
        {
            ToggleUpgradeMenu();
        }

        if (Input.GetKeyDown(KeyCode.H)) // Press 'H' to add 10 hack points for testing
        {
            playerUpgradeSystem.hackPoints += 10;
            Debug.Log("Added 10 hack points for testing.");
            UpdateUI();
        }
    }

    private void ToggleUpgradeMenu()
    {
        upgradeMenu.SetActive(!upgradeMenu.activeSelf);
        UpdateUI();
    }

    private void Upgrade(PlayerUpgradeSystem.UpgradeType type)
    {
        if (playerUpgradeSystem != null)
        {
            bool maxedOut = false;
            bool notEnoughPoints = false;

            // Check if the player has enough points and if the upgrade isn't maxed out
            if (playerUpgradeSystem.hackPoints < 1) // Check if the player has enough points
            {
                notEnoughPoints = true;
            }
            else
            {
                switch (type)
                {
                    case PlayerUpgradeSystem.UpgradeType.Health:
                        if (playerUpgradeSystem.playerHealth < 150f)
                            playerUpgradeSystem.Upgrade(type);
                        else
                            maxedOut = true;
                        break;
                    case PlayerUpgradeSystem.UpgradeType.Speed:
                        if (playerUpgradeSystem.playerSpeed < 7f)
                            playerUpgradeSystem.Upgrade(type);
                        else
                            maxedOut = true;
                        break;
                    case PlayerUpgradeSystem.UpgradeType.Armor:
                        if (playerUpgradeSystem.playerArmor < 50f)
                            playerUpgradeSystem.Upgrade(type);
                        else
                            maxedOut = true;
                        break;
                }
            }

            if (maxedOut)
            {
                StartCoroutine(ShowMaxedOutMessage());
            }
            else if (notEnoughPoints)
            {
                StartCoroutine(ShowNotEnoughPointsMessage());
            }

            UpdateUI();
        }
    }

    private IEnumerator ShowMaxedOutMessage()
    {
        maxedOutText.gameObject.SetActive(true);
        maxedOutText.alpha = 1f;
        maxedOutText.transform.localPosition = new Vector3(maxedOutText.transform.localPosition.x, -300f, maxedOutText.transform.localPosition.z); // Start lower by 300 units

        float fadeDuration = 1f;
        float timer = 0f;

        // Animate the fade and position drop
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, timer / fadeDuration);
            float dropAmount = Mathf.Lerp(0f, -20f, timer / fadeDuration); // Drop text by 20 units

            maxedOutText.alpha = alpha;
            maxedOutText.transform.localPosition = new Vector3(maxedOutText.transform.localPosition.x, -300f + dropAmount, maxedOutText.transform.localPosition.z); // Apply offset

            yield return null;
        }

        maxedOutText.gameObject.SetActive(false);
    }

    private IEnumerator ShowNotEnoughPointsMessage()
    {
        notEnoughPointsText.gameObject.SetActive(true);
        notEnoughPointsText.alpha = 1f;
        notEnoughPointsText.transform.localPosition = new Vector3(notEnoughPointsText.transform.localPosition.x, -300f, notEnoughPointsText.transform.localPosition.z); // Start lower by 300 units

        float fadeDuration = 1f;
        float timer = 0f;

        // Animate the fade and position drop
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, timer / fadeDuration);
            float dropAmount = Mathf.Lerp(0f, -20f, timer / fadeDuration); // Drop text by 20 units

            notEnoughPointsText.alpha = alpha;
            notEnoughPointsText.transform.localPosition = new Vector3(notEnoughPointsText.transform.localPosition.x, -300f + dropAmount, notEnoughPointsText.transform.localPosition.z); // Apply offset

            yield return null;
        }

        notEnoughPointsText.gameObject.SetActive(false);
    }

    private void UpdateUI()
    {
        if (playerUpgradeSystem == null) return;

        int points = playerUpgradeSystem.hackPoints;
        inGamePointsText.text = $"Hack Points: {points}";
        menuPointsText.text = $"Hack Points: {points}";
    }
}
