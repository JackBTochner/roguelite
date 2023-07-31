using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TramSpawn : MonoBehaviour
{
    public GameObject tram; // Tram object

    // Start is called before the first frame update
    void Start()
    {
        spawnTram();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void spawnTram()
    {
        // Spawn the tram in the position and rotation.
        Instantiate(tram, transform.position, transform.rotation);
        // Wait 3 seconds
        StartCoroutine(Wait(3));
    }

    IEnumerator Wait(float time)
    {
        // Any code here runs before the wait
        yield return new WaitForSeconds(time);
        // Any code here runs after the wait finishes.
        // Spawn the tram
        spawnTram();
    }
}
