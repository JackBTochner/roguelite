using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

public class BedroomActive : MonoBehaviour
{
    public GameObject storefront;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //hide storefront
            storefront.GetComponent<Renderer>().enabled = false;
            Debug.Log("trigger");
        }
    }
}

