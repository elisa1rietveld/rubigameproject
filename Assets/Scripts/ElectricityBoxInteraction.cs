using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ElectricityBoxInteraction : MonoBehaviour
{
    public Slider hackSlider;
    public static bool isHacking = false;
    private bool isHackingInProgress = false;
    public GameObject cameraObject;
    public float hackRange = 2f;
    private Transform player;
    private float hackProgress = 0f;

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
    }

    void Update()
    {
        if (isHacking && Input.GetKeyDown(KeyCode.C))
        {
            ToggleCamera();
        }

        if (isHackingInProgress)
        {
            hackProgress += Time.deltaTime / 5f;
            hackSlider.value = hackProgress;

            if (hackProgress >= 1f)
            {
                CompleteHack();
            }
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isHackingInProgress)
        {
            if (Vector3.Distance(player.position, transform.position) <= hackRange)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    StartHack();
                }
            }
        }
    }

    void StartHack()
    {
        isHackingInProgress = true;
        hackSlider.gameObject.SetActive(true);
        hackProgress = 0f;
    }

    void CompleteHack()
    {
        hackSlider.gameObject.SetActive(false);
        isHacking = true;
        isHackingInProgress = false;
        Debug.Log("Electricity box hacked!");
    }

    private void ToggleCamera()
    {
        cameraObject.SetActive(!cameraObject.activeSelf);
    }
}