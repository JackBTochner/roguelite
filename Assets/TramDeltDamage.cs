using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

public class TramDeltDamage : MonoBehaviour
{
    public float damage = 50.0f;
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
        if (other.CompareTag("Player"))
        {
            PlayerCharacter playerCharacter = other.gameObject.GetComponent<PlayerCharacter>();
            playerCharacter.takeDamage(damage);
        }
    }
}