using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

public class BedroomUnActive : MonoBehaviour
{
    public GameObject storefront;
    public float fallTime = 3.0f;
    public float fallDistance = 50.0f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //hide storefront and its children
            StartCoroutine(Fall());
        }
    }

    IEnumerator Fall()
    {
        Debug.Log("Trigger");

        storefront.transform.position = storefront.transform.position - new Vector3(0, 50, 0); // decrease y by 3
        yield return null;
    }
}
