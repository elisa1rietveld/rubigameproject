using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Vent : MonoBehaviour
{
    public GameObject teleportation;
    public float openDistance = 2.5f;
    public Transform teleport;
    private GameObject player;
    private Animator ventAnimator;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        ventAnimator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            float distance = Vector2.Distance(transform.position, player.transform.position);

            if (distance <= openDistance)
            {
                ventAnimator.SetBool("isOpen", true);
            }
            else 
            { 
              ventAnimator.SetBool("isOpen", false);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            player.transform.position = teleport.position;
            SceneManager.LoadScene(1);
        }
    }
}
