using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

public class MeleeWispAttackObject : MonoBehaviour
{
    public TransformAnchor playerTransformAnchor = default;
    public int damage = 1;
    private bool takedamage = true;

    void OnEnable()
    {
        takedamage = true;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")&& takedamage)
        {
            PlayerCharacter playerCharacter = playerTransformAnchor.Value.GetComponent<PlayerCharacter>();
            playerCharacter.TakeDamage(damage);
            //Player can take damage after 0.5f second
            StartCoroutine(DamageCooldown());
        }
    }
    IEnumerator DamageCooldown()
    {
        takedamage = false; 
        yield return new WaitForSeconds(0.5f);
        takedamage = true; 
    }
}
