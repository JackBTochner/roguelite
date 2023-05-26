using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

public class FrozenTrap : MonoBehaviour
{
    public float time = 2.0f;

    private CharacterController playerController; // Controller.

    private void OnTriggerEnter(Collider other)
    {
        // If the frozen trap touches the Player Collider.
        if (other.CompareTag("Player"))
        {
            // Get the other.gameObject component, which is the player component of the controller.
            playerController = other.gameObject.GetComponent<CharacterController>();
            // Turn of the player controller.
            DisablePlayerMovement();
            // Frozen trap life time.
            StartCoroutine(frozen());
        }
    }

    private void DisablePlayerMovement() // Turn off the player controller.
    {
        playerController.GetComponent<PlayerMovement>().allowMovement = false;
    }

    private void EnablePlayerMovement() // Turn on the player controller.
    {
        playerController.GetComponent<PlayerMovement>().allowMovement = true;
    }

    IEnumerator frozen()
    {
        // Wait till the time finished.
        yield return new WaitForSeconds(time);
        // Destroy the frozen trap.
        Destroy(gameObject);
        // Turn on the player controller.
        EnablePlayerMovement();
        // We destroy then turn on. The logic is , the frozen object is gone then the player can move.
    }
}
