using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class test : MonoBehaviour
{

    public GameObject interactionUI; // Assign the UI Image GameObject

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Make sure the player has a tag "Player"
        {
            interactionUI.SetActive(true); // Show the UI
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            interactionUI.SetActive(false); // Hide the UI
        }
    }

}


