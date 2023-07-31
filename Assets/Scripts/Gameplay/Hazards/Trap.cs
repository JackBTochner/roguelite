using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

public class Trap : MonoBehaviour
{
    public int damage = 25;

    private void OnTriggerEnter(Collider other)
    {
        // If the object touches the player collider
        if (other.CompareTag("Player"))
        {   
            // Debug print that it hits the collider
            Debug.Log("Take Damage From Trap");
            // Get the component of the player object "PlayerCharacter".
            PlayerCharacter playerCharacter = other.gameObject.GetComponent<PlayerCharacter>();
            // Take damage.
            playerCharacter.TakeDamage(damage);
        }
    }

}
