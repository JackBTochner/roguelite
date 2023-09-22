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
        // If hits other than enemy "and" other bullet, it will destroy the bullet.
        if(other.gameObject.tag != "Enemy" && other.gameObject.tag != "Bullet")
        {
            Destroy(gameObject);
        }

        // Destroy the bullet object.
        
    }
}
