using UnityEngine;

[CreateAssetMenu(fileName = "NewDialogue", menuName = "Dialogue/DialogueData")]
public class DialogueData : ScriptableObject
{
    [TextArea(3, 5)] public string[] dialogueLines; // Stores multiple dialogue sentences
}
