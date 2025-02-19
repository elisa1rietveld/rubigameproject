using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneTransition : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log("Trigger Detected with: " + collider.gameObject.name);
        

        if (collider.CompareTag("Player"))
        {
            Debug.Log("Player triggered! Transitioning to next scene.");
            SceneManager.LoadScene("StartMenu");
        }
    }
}