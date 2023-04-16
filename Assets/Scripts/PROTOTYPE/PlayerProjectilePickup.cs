using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Hittable))]
public class PlayerProjectilePickup : MonoBehaviour
{
    public float maxLifeTime = 5;
    private float timeSpawned = Mathf.NegativeInfinity;

    private void Start()
    {
        timeSpawned = Time.time;
        Debug.Log(timeSpawned);
    }
    private void Update()
    {
        if (Time.time > timeSpawned + maxLifeTime)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            AddToMagAndDestroy(player);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            AddToMagAndDestroy(other.gameObject);
        }
    }

    void AddToMagAndDestroy(GameObject player)
    { 
        if (player)
        { 
            PlayerWeaponController playerWeaponController = player.GetComponentInChildren<PlayerWeaponController>();
            playerWeaponController.AddToCurrentMagazine(1);
        }
        Destroy(this.transform.parent.gameObject);
    }
}
