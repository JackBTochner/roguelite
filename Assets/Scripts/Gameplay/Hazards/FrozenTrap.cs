using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

public class FrozenTrap : MonoBehaviour
{
    // Set trap off first.
    //public bool trapOn = false;
    // Set a float number for "time".
    public float time = 2.0f;
    // Get a CharacterController Object.
    private CharacterController playerController;

    private void OnTriggerEnter(Collider other)
    {
        // If the trap touches the player.
        if (other.CompareTag("Player"))
        {
            //if (!trapOn)
            //{
              //  trapOn = true;
                playerController = other.gameObject.GetComponent<CharacterController>();
                DisablePlayerMovement();
                StartCoroutine(frozen());
            //}
        }
    }

    private void DisablePlayerMovement() // Turn Off
    {
        playerController.enabled = false;
    }

    private void EnablePlayerMovement() // Turn On
    {
        playerController.enabled = true;
    }

    IEnumerator frozen()
    {
        yield return new WaitForSeconds(time);
        // Destroy first, then player can move.
        Destroy(gameObject);
        
        EnablePlayerMovement();
    }
}
