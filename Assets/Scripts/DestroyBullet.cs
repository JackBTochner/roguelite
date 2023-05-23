using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

public class DestroyBullet : MonoBehaviour
{
    public int damage = 5;
    // Start is called before the first frame update
    

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            PlayerCharacter PlayerCharacter = other.gameObject.GetComponent<PlayerCharacter>();
            PlayerCharacter.TakeDamage(damage);
            //Debug.Log("Bullet hits!");
            Destroy(gameObject);
        }
    }
}
