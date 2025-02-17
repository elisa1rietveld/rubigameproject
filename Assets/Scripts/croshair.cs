using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class croshair : MonoBehaviour

{
    public SpriteRenderer crosshairSprite;

    void Update()
    {
        Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        crosshairSprite.transform.position = new Vector3(mouseWorldPos.x, mouseWorldPos.y, 0);
    }
}
