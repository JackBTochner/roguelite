using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    public HubNPC currentTarget;

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Interactable"))
        {
            EnableInteractPrompt();
        }
    }


    public void OnTriggerExit() 
    {
        currentTarget = null;
        DisableInteractPrompt();
    }

    public void EnableInteractPrompt()
    { 
        // show ui

    }

    public void DisableInteractPrompt() 
    { 
        // hide UI
    }
}
