using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

public class TramDeltDamage : MonoBehaviour
{
    public int damage = 50;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other) 
    {
        // When the tram hits the player collider
        if (other.CompareTag("Player"))
        {
            // Get the player object component "PlayerCharacter".
            PlayerCharacter playerCharacter = other.gameObject.GetComponent<PlayerCharacter>();
            // Take damage
            playerCharacter.TakeDamage(damage);
        }
    }
}