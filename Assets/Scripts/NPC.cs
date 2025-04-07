using UnityEngine;
using TMPro;

public class NPC : MonoBehaviour
{
    public DialogueManager dialogueManager;
    public DialogueData dialogueData; // Assign a unique Scriptable Object
    public TextMeshProUGUI interactionText;
    public LayerMask playerLayer;

    private bool isPlayerNearby = false;

    void Start()
    {
        interactionText.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (((1 << other.gameObject.layer) & playerLayer) != 0)
        {
            isPlayerNearby = true;
            interactionText.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (((1 << other.gameObject.layer) & playerLayer) != 0)
        {
            isPlayerNearby = false;
            interactionText.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        // Check for 'I' key press using KeyCode
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.I))
        {
            interactionText.gameObject.SetActive(false);
            if (dialogueData != null)
            {
                dialogueManager.StartDialogue(dialogueData);
            }
        }
    }
}
