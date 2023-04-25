using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

public class Trap : MonoBehaviour
{
    public int damage = 25;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Take Damage From Trap");
            PlayerCharacter playerCharacter = other.gameObject.GetComponent<PlayerCharacter>();
            playerCharacter.TakeDamage(damage);
        }
    }

}
