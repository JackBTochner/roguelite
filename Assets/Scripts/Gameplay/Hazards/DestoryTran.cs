using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestoryTram : MonoBehaviour
{
    public float sideBound = 30;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // If the tram goes out of z range, delete the object.
        if(transform.position.z > sideBound)
        {
            Destroy(gameObject);
        }
    }
    
}
