using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

public class SlimeDamage : MonoBehaviour
{
    void OnTriggerStay(Collider other)
    {
        // If the Slime hits and keep hitting the player.
        if (other.gameObject.tag == "Player")
        {
            // Get the player object component "PlayerCharacter";
            PlayerCharacter PlayerCharacter = other.gameObject.GetComponent<PlayerCharacter>();
            //Script "SlimeDamageManager.cs" this script has the take damage function, pass our character to the function.
            SlimeDamageManager.ApplyDamage(PlayerCharacter);
        }
    }
}
