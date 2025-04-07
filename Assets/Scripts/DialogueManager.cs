using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

public class DialogueManager : MonoBehaviour
{
    public GameObject dialogueBox; // Reference to UI panel
    public TextMeshProUGUI dialogueText; // Text component for displaying dialogue
    public Button continueButton; // Clickable button

    private Queue<string> dialogueQueue = new Queue<string>();

    void Start()
    {
        dialogueBox.SetActive(false); // Hide the dialogue box initially
        continueButton.onClick.AddListener(DisplayNextSentence);
    }

    public void StartDialogue(DialogueData dialogueData)
    {
        dialogueQueue.Clear();

        foreach (string sentence in dialogueData.dialogueLines)
        {
            dialogueQueue.Enqueue(sentence);
        }

        dialogueBox.SetActive(true);
        DisplayNextSentence();
    }

    void DisplayNextSentence()
    {
        if (dialogueQueue.Count == 0)
        {
            EndDialogue();
            return;
        }

        dialogueText.text = dialogueQueue.Dequeue();
    }

    void EndDialogue()
    {
        dialogueBox.SetActive(false);
    }
}
