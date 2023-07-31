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
            //hide storefront and its children
            foreach (Renderer r in storefront.GetComponentsInChildren<Renderer>())
            {
                r.enabled = true;
            }
            Debug.Log("Double trigger");
        }
    }
}