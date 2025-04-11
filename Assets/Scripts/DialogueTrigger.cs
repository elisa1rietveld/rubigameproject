using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public DialogueData dialogueData;
    public DialogueManager dialogueManager;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            dialogueManager.StartDialogue(dialogueData);
        }
    }
}
