using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{

    public Transform spawnPoint;

    [SerializeField] private float timer = 5.0f;

    private float bulletTime;

    public GameObject enemyBullet;

    public float enemySpeed;

    public Transform facePlayer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {   //1. Face the Player for always. Problem now: it will face the player position when hits the max follow range,
        //when player move in this range the enemy will keep focusing on the first position, not the update position.
        transform.LookAt(facePlayer);
        ShootAtPlayer();
    }
    void ShootAtPlayer()
    {
        bulletTime -= Time.deltaTime;

        if (bulletTime > 0) return;

        bulletTime = timer;

        GameObject bulletObj = Instantiate(enemyBullet, spawnPoint.transform.position, spawnPoint.transform.rotation) as GameObject;

        Rigidbody bulletRig = bulletObj.GetComponent<Rigidbody>();
        bulletRig.AddForce(-bulletRig.transform.forward * -enemySpeed);
        Destroy(bulletObj, 5f);
    }

    
}
