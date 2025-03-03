using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHiding : MonoBehaviour
{
    private bool isHidden = false;
    private List<EnemyPatrol> enemiesInScene = new List<EnemyPatrol>();

    void Start()
    {
        EnemyPatrol[] enemies = FindObjectsOfType<EnemyPatrol>();
        foreach (var enemy in enemies)
        {
            enemiesInScene.Add(enemy);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("HidingSpot"))
        {
            isHidden = true;
            UpdateEnemyDetection();
            ChangeOpacity(other.gameObject, 0.5f); // Reduce opacity to 50%
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("HidingSpot"))
        {
            isHidden = false;
            UpdateEnemyDetection();
            ChangeOpacity(other.gameObject, 1f); // Restore full opacity
        }
    }

    void UpdateEnemyDetection()
    {
        foreach (var enemy in enemiesInScene)
        {
            enemy.SetPlayerHidden(isHidden);  // This updates playerIsHidden inside the enemy
        }
    }


    void ChangeOpacity(GameObject hidingSpot, float alpha)
    {
        SpriteRenderer renderer = hidingSpot.GetComponent<SpriteRenderer>();
        if (renderer != null)
        {
            Color color = renderer.color;
            color.a = alpha;
            renderer.color = color;
        }
    }
}
