using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class croshair : MonoBehaviour

{
    // Reference to the SpriteRenderer for the crosshair
    public SpriteRenderer crosshairSprite;

    void Update()
    {
        // Get the mouse position in world space
        Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Update the position of the crosshair to the mouse position
        crosshairSprite.transform.position = new Vector3(mouseWorldPos.x, mouseWorldPos.y, 0);
    }
}
