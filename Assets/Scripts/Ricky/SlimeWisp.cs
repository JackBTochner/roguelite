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


    // Volcano Eruption
    // Need to add a rigidbody to the slime and turn on gravity, we don't want it to affect the slime and the enemy. -
    // - So we need to create a layer: Edit->Project Settings->Search for "Tags and Layers"->in "Layers" we add a Layer and name it.
    // Then: Edit->Project Settings->Search for "Physics"->Find "Layer Collision Matrix"->Open it->untick things we don't want to affect.

    // Each 3 second a eruption will happen.
    public float eruptionInterval = 3.0f;
    // The next eruption time.
    private float nextEruptionTime;
    // The slime that eruption out.
    public int slimeEruption = 5;
    // 
    public float EruptionForce = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        // The first eruption happens after the current time + 3 second.
        nextEruptionTime = Time.time + eruptionInterval;
    }

    // Update is called once per frame
    void Update()
    {
        //Checks the the game time is bigger than the timenow
        if (Time.time > timenow)
        {
            SpawnSlime();
            // If spawn we add, game time + 0.2, in the next frame, it will check is it inside the 0.2? To check spawning the next one.
            timenow = Time.time + SpawnSlimeTime;
        }

        //If the current time is more than the next eruption time we set then make new eruption
        if (Time.time > nextEruptionTime)
        {
            // Each loop will erupted 1 slime
            for (int i = 0; i < slimeEruption; i++)
            {
                // Spawn a slime, return a reference to it, this reference is stored in to the slime variable.
                GameObject slime = SpawnSlime();
                // Get the rigidbody of the slime.
                Rigidbody rb = slime.GetComponent<Rigidbody>();
                // Checks the slime has a rigidbody
                if (rb)
                {
                    // Create a random direction Vector3 
                    Vector3 randomDirection = new Vector3(
                        // x of -1f to 1f
                        Random.Range(-1f, 1f),
                        // y of 0.5f to 1.5f
                        Random.Range(0.5f, 1.5f),
                        // z of -1f to 1f
                        Random.Range(-1f, 1f)
                        //A vector that points in the same direction as the original but has a precise length or magnitude of 1.
                        ).normalized;
     // Add force to the slime/
     // RandomDirection * EruptionForce get the direction and ForceMode.Impulse to push the slime to that direction with the power of 10
                    rb.AddForce(randomDirection * EruptionForce, ForceMode.Impulse);
                }
            }
            // Reset the next eruption time.
            nextEruptionTime = Time.time + eruptionInterval;
        }
    }

    public GameObject SpawnSlime()
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
        // The erupotion method can get a reference to this object.
        return slime;
    }
}