using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideRoomAtStart : MonoBehaviour
{
    // The store front game object.
    public GameObject storefront;
    // The distance we want it to be.
    public float fallDistance = -100.0f;

    // Start is called before the first frame update
    void Start()
    {
        //At the start of the game, move the store front down 100.
        storefront.transform.position = storefront.transform.position + new Vector3(0, fallDistance, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
