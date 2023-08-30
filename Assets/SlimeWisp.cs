using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeWisp : MonoBehaviour
{
    public Transform SlimeSpawnPoint;
    public GameObject Slime;
    public float SlimeTime = 5.0f;
    public float SpawnSlimeTime = 0.2f;
    public float timenow;
    public float Slimey = 0.2f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > timenow)
        {
            Vector3 spawnPosition = SlimeSpawnPoint.position;
            spawnPosition.y = Slimey;
            GameObject slime = Instantiate(Slime, spawnPosition, Slime.transform.rotation) as GameObject;
            Destroy(slime, SlimeTime);
            timenow = Time.time + SpawnSlimeTime;
        }
    }
}
