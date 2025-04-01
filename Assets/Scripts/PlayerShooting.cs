using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class PlayerShooting : MonoBehaviour
{ 
    public GameObject bulletPrefab;   
    public Transform shootPoint; 
    public float shootRate = 0.5f; 
    public int maxBullets = 10; 
    private int currentBullets; 
    private float lastShootTime;

    public TextMeshProUGUI bulletCountText; 

    void Start()
    {
        currentBullets = maxBullets; 
        UpdateBulletCountText(); 
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1) && Time.time - lastShootTime >= shootRate && currentBullets > 0)
{
    ShootBullet();
}
    }

    void ShootBullet()
    {
        Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);
        currentBullets--;
        UpdateBulletCountText();
        lastShootTime = Time.time;
    }

    void UpdateBulletCountText()
    {
        bulletCountText.text = "Bullets: " + currentBullets.ToString(); 
    }

    public void RefillBullets()
    {
        currentBullets = maxBullets; 
        UpdateBulletCountText(); 
    }
}