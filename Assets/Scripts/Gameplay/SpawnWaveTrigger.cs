using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class SpawnWaveTrigger : MonoBehaviour
{
    public UnityEvent<Collider> playerEnteredTrigger;

    public void OnEnable()
    {
        GetComponent<Collider>().isTrigger = true;
        GetComponent<Rigidbody>().isKinematic = true;
    }

    public void OnTriggerEnter(Collider col)
    {
        if (playerEnteredTrigger != null && col.CompareTag("Player"))
        {
            playerEnteredTrigger.Invoke(col);
        }
    }
}
