using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class OpenWorkBench : MonoBehaviour
{
    public GameObject workBenchUI;

    void OnTriggerEnter(Collider other)
    {
        // If the player interacts with the WorkBench
        if(other.tag == "Player")
            // Displays WorkBench UI
            workBenchUI.SetActive(true);
    }
}

