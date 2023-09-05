using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

public class DestroyBullet : MonoBehaviour
{
    public int damage = 25;
    // Start is called before the first frame update

    void OnTriggerEnter(Collider other)
    {
        // If the bullet touches the player.
        if (other.gameObject.tag == "Player")
        {
            // Get the player object component "PlayerCharacter";
            PlayerCharacter PlayerCharacter = other.gameObject.GetComponent<PlayerCharacter>();
            // Take damage.
            PlayerCharacter.TakeDamage(damage);
            
        }
        if(other.gameObject.tag != "Enemy")
        {
            Destroy(gameObject);
        }
        // Destroy the bullet object.
        
    }
}
