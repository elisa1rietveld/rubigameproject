using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHiding : MonoBehaviour
{
    private bool isHidden = false;
    private List<EnemyMovement> enemiesInScene = new List<EnemyMovement>();

    void Start()
    {
        EnemyMovement[] enemies = FindObjectsOfType<EnemyMovement>();
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
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("HidingSpot"))
        {
            isHidden = false;
            UpdateEnemyDetection();
        }
    }

    void UpdateEnemyDetection()
    {
        foreach (var enemy in enemiesInScene)
        {
            if (isHidden)
                enemy.StopFollowingPlayer();
            else
                enemy.StartFollowingPlayer();
        }
    }
}
