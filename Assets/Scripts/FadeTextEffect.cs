using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FadeTextEffect : MonoBehaviour
{
    public TextMeshProUGUI text;  // Assign your TextMeshPro UI element
    public float stayDuration = 5f;   // Time the text stays visible before fading
    public float fadeDuration = 1.5f; // Time for fade out
    public float moveDistance = 20f;  // Distance the text moves down

    void Start()
    {
        text.canvasRenderer.SetAlpha(0f);  // Ensure text starts invisible
        StartCoroutine(ShowAndFade());
    }

    IEnumerator ShowAndFade()
    {
        text.canvasRenderer.SetAlpha(1f);  // Make text visible
        Vector3 startPosition = text.rectTransform.anchoredPosition;
        Vector3 endPosition = startPosition - new Vector3(0, moveDistance, 0);

        yield return new WaitForSeconds(stayDuration);  // Wait before fading

        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = 1 - (elapsedTime / fadeDuration);  // Gradually fade out
            text.canvasRenderer.SetAlpha(alpha);

            // Lerp position downward
            text.rectTransform.anchoredPosition = Vector3.Lerp(startPosition, endPosition, elapsedTime / fadeDuration);

            yield return null;
        }

        // Ensure final values are set
        text.canvasRenderer.SetAlpha(0f);
        text.rectTransform.anchoredPosition = endPosition;
    }
}
