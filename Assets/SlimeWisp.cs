using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeWisp : MonoBehaviour
{
    // The SpawnPoint of Slime Wisp
    public Transform SlimeSpawnPoint;
    // The object of Slime
    public GameObject Slime;
    // Slime stay for 5 seconds
    public float SlimeTime = 5.0f;
    // Slime will be spawn each 0.2 seconds.
    public float SpawnSlimeTime = 0.2f;
    // Time calculation
    public float timenow;
    // Set all slime spawn to 0.2 y
    public float Slimey = 0.4f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Checks the the game time is bigger than the timenow
        if (Time.time > timenow)
        {
            // get the position of the spawing point of slime
            Vector3 spawnPosition = SlimeSpawnPoint.position;

            RaycastHit hit;
            // How far the ray can go.The maximum it can go.
            float maxRaycastDistance = 100f;
            // Check if it hits something, using the spawing point spawing down 
            // out hit store hit information in the hit variable.
            if (Physics.Raycast(SlimeSpawnPoint.position, Vector3.down, out hit, maxRaycastDistance))
            {
                // The information store in the hit will be the spawing point of this slime.
                spawnPosition = hit.point;
                // Spawing above a bit of that hitting area(spawning area).
                spawnPosition.y += Slimey;
            }

            // Spawn slime
            GameObject slime = Instantiate(Slime, spawnPosition, Slime.transform.rotation) as GameObject;
            // Destory after SlimeTime
            Destroy(slime, SlimeTime);
            // If spawn we add, game time + 0.2, in the next frame, it will check is it inside the 0.2? To check spawning the next one.
            timenow = Time.time + SpawnSlimeTime;
        }
    }
}
