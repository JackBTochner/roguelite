using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;
using Pathfinding; // Using "Pathfinding" for AIPath

public class Explodetest : MonoBehaviour
{
    public TransformAnchor playerTransformAnchor = default; // When the player spawn it will assign it to the player, it can get the current player, transform and rotation.
    public GameObject explosionPrefab; // The Explosion animation.
    public float triggerDistance = 1.0f; // The distance.
    public int fulldamage = 25; 
    public int middamage = 10;
    public float normalSpeed = 3.0f; // After the trigger speed.
    private AIPath aiPath; 

    void Update()
    {   
        // Let the explodeEnemy face the Player all the time.
        transform.LookAt(playerTransformAnchor.Value);
        // The player is not spawnning immediately, so it may cause errors, added a if statement to check, the - 
        // - playerTransformAnchor.value is assign to the player object.
        if (playerTransformAnchor != null && playerTransformAnchor.Value != null)
        {
            //If the object hits the distance with the playe, it will do....
            if (Vector3.Distance(transform.position, playerTransformAnchor.Value.position) < triggerDistance)
            {
                // Get the current object component "AIPath".
                aiPath = GetComponent<AIPath>();

                // Set the AIPath maxSpeed, slow the explodeEnemy.
                aiPath.maxSpeed = normalSpeed;


                StartCoroutine(WaitForExplode(2.0f)); //Do after 2 seconds.
            }
        }
    }

    IEnumerator WaitForExplode(float time)
    {
        // Trigger then do...
        yield return new WaitForSeconds(time);
        // Do the explosion animation.
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        // KILL
        GetComponent<Enemy>().Die();
        // let player take damage, when in the damage distance.
        // Get the current value of the player component "PlayerCharacter".
        PlayerCharacter playerCharacter = playerTransformAnchor.Value.GetComponent<PlayerCharacter>();
        if (Vector3.Distance(transform.position, playerTransformAnchor.Value.position) < 3f) // Take full damage.
        {   
            playerCharacter.TakeDamage(fulldamage);
        }
        else if (Vector3.Distance(transform.position, playerTransformAnchor.Value.position) < 6f) // Take mid damage.
        {
            playerCharacter.TakeDamage(middamage);
        }
    }
}
