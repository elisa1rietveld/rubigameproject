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

        healthButton.onClick.AddListener(() => TryUpgrade(PlayerUpgradeSystem.UpgradeType.Health, 150f, ref maxedOutCoroutine));
        speedButton.onClick.AddListener(() => TryUpgrade(PlayerUpgradeSystem.UpgradeType.Speed, 7f, ref maxedOutCoroutine));
        armorButton.onClick.AddListener(() => TryUpgrade(PlayerUpgradeSystem.UpgradeType.Armor, 50f, ref maxedOutCoroutine));

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
        upgradeMenu.SetActive(!upgradeMenu.activeSelf);
        UpdateUI();
    }

    private void TryUpgrade(PlayerUpgradeSystem.UpgradeType type, float maxValue, ref Coroutine messageCoroutine)
    {
        if (playerUpgradeSystem == null) return;

        if (playerUpgradeSystem.hackPoints < playerUpgradeSystem.upgradeCost)
        {
            ShowMessage(notEnoughPointsText, ref notEnoughCoroutine, notEnoughStartPos);
            return;
        }

        float currentValue = type switch
        {
            PlayerUpgradeSystem.UpgradeType.Health => playerUpgradeSystem.playerHealth,
            PlayerUpgradeSystem.UpgradeType.Speed => playerUpgradeSystem.playerSpeed,
            PlayerUpgradeSystem.UpgradeType.Armor => playerUpgradeSystem.playerArmor,
            _ => 0f // als geen van de verwachte typen overeenkomt, krijgt currentValue de waarde 0
        };

        if (currentValue < maxValue)
            playerUpgradeSystem.Upgrade(type);
        else
            ShowMessage(maxedOutText, ref maxedOutCoroutine, maxedOutStartPos);

        UpdateUI();
    }

    private void ShowMessage(TMP_Text messageText, ref Coroutine messageCoroutine, Vector3 startPos)
    {
        // reset positie voordat de animatie start
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
            messageText.alpha = Mathf.Lerp(1f, 0f, progress); // lerp zorgt ervoor dat hij soepeler gaat van a naar b
            messageText.rectTransform.localPosition = Vector3.Lerp(startPos, endPos, progress);
            yield return null;
        }

        messageText.gameObject.SetActive(false);
        messageText.rectTransform.localPosition = startPos; // terug naar startpositie
    }

    private void UpdateUI()
    {
        if (playerUpgradeSystem == null) return;

        inGamePointsText.text = $"Hack Points: {playerUpgradeSystem.hackPoints}"; //string interpolation
        menuPointsText.text = $"Hack Points: {playerUpgradeSystem.hackPoints}";
    }
}
