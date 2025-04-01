using UnityEngine;

public class MenuToggle : MonoBehaviour
{
    public GameObject menuPanel; // Assign your UI panel in the Inspector.

    void Start()
    {
        menuPanel.SetActive(false); // Start with the menu hidden.
    }

    public void ToggleMenu()
    {
        menuPanel.SetActive(!menuPanel.activeSelf);
    }
}
