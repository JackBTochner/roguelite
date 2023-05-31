using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectilePickup : MonoBehaviour
{
    public float maxLifeTime = 5;
    private float timeSpawned = Mathf.NegativeInfinity;

    private void Start()
    {
        timeSpawned = Time.time;
    }
    private void Update()
    {
        if (Time.time > timeSpawned + maxLifeTime)
        {
            GameObject Launcher = GameObject.FindGameObjectWithTag("ProjectileLauncher");
            AddToMagAndDestroy(Launcher);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "ProjectileLauncher")
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
