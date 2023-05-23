using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

public class Explodetest : MonoBehaviour
{
    public GameObject target; // The player.
    public GameObject explosionPrefab; // The Explosion animation.
    public float triggerDistance = 1.0f; // The distance.
    public int fulldamage = 50;
    public int middamage = 25;

    void Update()
    {
        //If the object hits the distance with the playe, it will do....
        if (Vector3.Distance(transform.position, target.transform.position) < triggerDistance)
        {
            //Do this 
            StartCoroutine(WaitForExplode(2.0f)); //Do after 2 seconds.

        }
    }

    IEnumerator WaitForExplode(float time)
    {
        yield return new WaitForSeconds(time);

        Instantiate(explosionPrefab, transform.position, Quaternion.identity); // Do the explosion.
        Destroy(gameObject); // Delete the explosion enemy.

        if (Vector3.Distance(transform.position, target.transform.position) < 3f)
        {
            PlayerCharacter playerCharacter = target.GetComponent<PlayerCharacter>();
            playerCharacter.TakeDamage(fulldamage);
        }
        else if (3f < Vector3.Distance(transform.position, target.transform.position) && Vector3.Distance(transform.position, target.transform.position) < 6f)
        {
            PlayerCharacter playerCharacter = target.GetComponent<PlayerCharacter>();
            playerCharacter.TakeDamage(middamage);
        }
    }
}
