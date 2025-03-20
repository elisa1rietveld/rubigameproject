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

            if (maxedOut)
            {
                StartCoroutine(ShowMaxedOutMessage());
            }

            UpdateUI();
        }
    }

    private IEnumerator ShowMaxedOutMessage()
    {
        maxedOutText.gameObject.SetActive(true);
        maxedOutText.alpha = 1f;

        yield return new WaitForSeconds(.5f);

        float fadeDuration = 1f;
        float startAlpha = 1f;
        float timer = 0f;

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, 0f, timer / fadeDuration);
            maxedOutText.alpha = alpha;
            yield return null;
        }

        maxedOutText.gameObject.SetActive(false);
    }

    private void UpdateUI()
    {
        if (playerUpgradeSystem == null) return;

        int points = playerUpgradeSystem.hackPoints;
        inGamePointsText.text = $"Hack Points: {points}";
        menuPointsText.text = $"Hack Points: {points}";
    }
}