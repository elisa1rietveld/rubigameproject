using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class UIUpgradeMenu : MonoBehaviour
{
    public GameObject upgradeMenu;
    public GameObject inGameUI;
    public TMP_Text menuPointsText;

    public Button healthButton;
    public Button speedButton;
    public Button armorButton;

    public TMP_Text healthProgressText;
    public TMP_Text speedProgressText;
    public TMP_Text armorProgressText;

    public TMP_Text maxedOutText;
    public TMP_Text notEnoughPointsText;

    private PlayerUpgradeSystem playerUpgradeSystem;
    private Coroutine maxedOutCoroutine;
    private Coroutine notEnoughCoroutine;

    private Vector3 maxedOutStartPos;
    private Vector3 notEnoughStartPos;

    void Start()
    {
        playerUpgradeSystem = GameObject.FindWithTag("Player")?.GetComponent<PlayerUpgradeSystem>();

        upgradeMenu.SetActive(false);
        maxedOutText.gameObject.SetActive(false);
        notEnoughPointsText.gameObject.SetActive(false);

        maxedOutStartPos = maxedOutText.rectTransform.localPosition;
        notEnoughStartPos = notEnoughPointsText.rectTransform.localPosition;

        healthButton.onClick.AddListener(() => TryUpgrade(PlayerUpgradeSystem.UpgradeType.Health, playerUpgradeSystem.maxHealth, ref maxedOutCoroutine));
        speedButton.onClick.AddListener(() => TryUpgrade(PlayerUpgradeSystem.UpgradeType.Speed, playerUpgradeSystem.maxSpeed, ref maxedOutCoroutine));
        armorButton.onClick.AddListener(() => TryUpgrade(PlayerUpgradeSystem.UpgradeType.Armor, playerUpgradeSystem.maxArmor, ref maxedOutCoroutine));

        UpdateUI();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
            ToggleUpgradeMenu();

        if (Input.GetKeyDown(KeyCode.H))
        {
            playerUpgradeSystem.hackPoints += 10;
            UpdateUI();
        }
    }

    private void ToggleUpgradeMenu()
    {
        bool isOpening = !upgradeMenu.activeSelf;
        upgradeMenu.SetActive(isOpening);
        if (inGameUI != null)
            inGameUI.SetActive(!isOpening);

        UpdateUI();
    }

    private void TryUpgrade(PlayerUpgradeSystem.UpgradeType type, float maxValue, ref Coroutine messageCoroutine)
    {
        if (playerUpgradeSystem == null) return;

        float currentValue = type switch
        {
            PlayerUpgradeSystem.UpgradeType.Health => playerUpgradeSystem.playerHealth,
            PlayerUpgradeSystem.UpgradeType.Speed => playerUpgradeSystem.playerSpeed,
            PlayerUpgradeSystem.UpgradeType.Armor => playerUpgradeSystem.playerArmor,
            _ => 0f
        };

        if (currentValue >= maxValue)
        {
            ShowMessage(maxedOutText, ref maxedOutCoroutine, maxedOutStartPos);
            return;
        }

        if (playerUpgradeSystem.hackPoints < playerUpgradeSystem.upgradeCost)
        {
            ShowMessage(notEnoughPointsText, ref notEnoughCoroutine, notEnoughStartPos);
            return;
        }

        playerUpgradeSystem.Upgrade(type);
        UpdateUI();
    }

    private void ShowMessage(TMP_Text messageText, ref Coroutine messageCoroutine, Vector3 startPos)
    {
        messageText.rectTransform.localPosition = startPos;

        if (messageCoroutine != null)
            StopCoroutine(messageCoroutine);
        messageCoroutine = StartCoroutine(FadeOutMessage(messageText, startPos));
    }

    private IEnumerator FadeOutMessage(TMP_Text messageText, Vector3 startPos)
    {
        messageText.gameObject.SetActive(true);
        messageText.alpha = 1f;

        Vector3 endPos = startPos + new Vector3(0f, -20f, 0f);
        float duration = 1f;

        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            float progress = t / duration;
            messageText.alpha = Mathf.Lerp(1f, 0f, progress);
            messageText.rectTransform.localPosition = Vector3.Lerp(startPos, endPos, progress);
            yield return null;
        }

        messageText.gameObject.SetActive(false);
        messageText.rectTransform.localPosition = startPos;
    }

    private void UpdateUI()
    {
        if (playerUpgradeSystem == null) return;

        menuPointsText.text = $"Hack Points: {playerUpgradeSystem.hackPoints}";

        healthProgressText.text = $"Health: {playerUpgradeSystem.playerHealth} / {playerUpgradeSystem.maxHealth}";
        speedProgressText.text = $"Speed: {playerUpgradeSystem.playerSpeed} / {playerUpgradeSystem.maxSpeed}";
        armorProgressText.text = $"Armor: {playerUpgradeSystem.playerArmor} / {playerUpgradeSystem.maxArmor}";
    }
}
