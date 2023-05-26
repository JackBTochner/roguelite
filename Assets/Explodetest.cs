using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

public class Explodetest : MonoBehaviour
{
    public TransformAnchor playerTransformAnchor = default; // When the player spawn it will assign it to the player, it can get the current player, transform and rotation.
    public GameObject explosionPrefab; // The Explosion animation.
    public float triggerDistance = 1.0f; // The distance.
    public int fulldamage = 25; 
    public int middamage = 10;


    void Update()
    {
        //If the object hits the distance with the playe, it will do....
        if (Vector3.Distance(transform.position, playerTransformAnchor.Value.position) < triggerDistance)
        {
            //Do this 
            StartCoroutine(WaitForExplode(2.0f)); //Do after 2 seconds.
        }
    }

    IEnumerator WaitForExplode(float time)
    {
        // Trigger then do...
        yield return new WaitForSeconds(time);
        // Do the explosion animation.
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        // Destroy the explodeEnemy.
        Destroy(gameObject);
        // let player take damage, when in the damage distance.
        // Get the current value of the player component "PlayerCharacter".
        PlayerCharacter playerCharacter = playerTransformAnchor.Value.GetComponent<PlayerCharacter>();
        if (Vector3.Distance(transform.position, playerTransformAnchor.Value.position) < 3f) // Take full damage.
        {   
            playerCharacter.TakeDamage(fulldamage);
        }
        else if (Vector3.Distance(transform.position, playerTransformAnchor.Value.position) < 10f) // Take mid damage.
        {
            playerCharacter.TakeDamage(middamage);
        }
    }
}
